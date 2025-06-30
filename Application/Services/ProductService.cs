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

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            IValidationService validationService)
        {
            _repository = repository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetProductDto>>(products);
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
            await _repository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}