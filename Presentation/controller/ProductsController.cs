using Microsoft.AspNetCore.Mvc;
using Domain;
using Application;
using AutoMapper;
using FluentValidation;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductsController(IProductRepository repository, IMapper mapper, IValidator<CreateProductDto> validator, IValidator<CreateProductDto> createValidator, IValidator<UpdateProductDto> updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GetProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<GetProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            var product = _mapper.Map<Product>(dto);
            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();
            var created = await _repository.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<GetProductDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            _mapper.Map(dto, product);
            await _repository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}