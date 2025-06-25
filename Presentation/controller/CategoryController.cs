using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application;

namespace API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            try
            {
                var createdCategory = await _categoryService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetAll), createdCategory);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            try
            {
                var success = await _categoryService.UpdateAsync(dto);
                if (success)
                    return NoContent();
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (success)
                return NoContent();
            return NotFound();
        }
    }
}