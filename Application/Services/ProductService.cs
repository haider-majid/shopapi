using AutoMapper;
using Domain;
using Application;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly ICacheService _cacheService;

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            IValidationService validationService,
            ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var cacheKey = _cacheService.GenerateKey("product", "getall");

            // Try to get from cache first
            var cachedProducts = await _cacheService.GetAsync<IEnumerable<GetProductDto>>(cacheKey);
            if (cachedProducts != null)
                return cachedProducts;

            // If not in cache, get from database
            var products = await _repository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<GetProductDto>>(products);

            // Cache the result
            await _cacheService.SetAsync(cacheKey, productDtos);

            return productDtos;
        }

        public async Task<GetProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product != null ? _mapper.Map<GetProductDto>(product) : null;
        }

        public async Task<GetProductDto> CreateAsync(CreateProductDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var product = _mapper.Map<Product>(dto);
            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            var created = await _repository.AddAsync(product);

            // Invalidate cache after creating new product
            await InvalidateProductCache();

            return _mapper.Map<GetProductDto>(created);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            if (dto.Name != null)
                existingProduct.Name = dto.Name;
            if (dto.Description != null)
                existingProduct.Description = dto.Description;
            if (dto.Price != null)
                existingProduct.Price = dto.Price.Value;
            if (dto.Stock != null)
                existingProduct.Stock = dto.Stock.Value;

            await _repository.UpdateAsync(existingProduct);

            // Invalidate cache after updating product
            await InvalidateProductCache();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return false;

            await _repository.DeleteAsync(id);

            // Invalidate cache after deleting product
            await InvalidateProductCache();

            return true;
        }

        private async Task InvalidateProductCache()
        {
            var cacheKey = _cacheService.GenerateKey("product", "getall");
            await _cacheService.RemoveAsync(cacheKey);
        }
    }
}