

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;

        public CategoryController(ICategoryRepository repository, IMapper mapper, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<GetCategoryDto>>(categories);
            return Ok(dtos);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            var createdDto = _mapper.Map<GetCategoryDto>(category);
            return CreatedAtAction(nameof(GetAll), createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var category = _mapper.Map<Category>(dto);
            await _repository.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}