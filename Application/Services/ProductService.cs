using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly ICacheService _cacheService;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidationService validationService,
            ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
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
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<GetProductDto>>(products);

            // Cache the result
            await _cacheService.SetAsync(cacheKey, productDtos);

            return productDtos;
        }

        public async Task<GetProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            return product != null ? _mapper.Map<GetProductDto>(product) : null;
        }

        public async Task<GetProductDto> CreateAsync(CreateProductDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var productName = new ProductName(dto.Name);
            var price = new Money(dto.Price);
            var product = Product.Create(productName, dto.Description, price, dto.Stock, dto.CategoryId);

            var created = await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            // Invalidate cache after creating new product
            await InvalidateProductCache();

            return _mapper.Map<GetProductDto>(created);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            if (dto.Name != null || dto.Description != null)
            {
                var productName = dto.Name != null ? new ProductName(dto.Name) : existingProduct.Name;
                var description = dto.Description ?? existingProduct.Description;
                existingProduct.UpdateDetails(productName, description);
            }

            if (dto.Price != null)
            {
                var newPrice = new Money(dto.Price.Value);
                existingProduct.UpdatePrice(newPrice);
            }

            if (dto.Stock != null)
            {
                var stockDifference = dto.Stock.Value - existingProduct.Stock;
                if (stockDifference != 0)
                {
                    existingProduct.AdjustStock(stockDifference, "Manual stock adjustment");
                }
            }

            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            // Invalidate cache after updating product
            await InvalidateProductCache();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            await _unitOfWork.ProductRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

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