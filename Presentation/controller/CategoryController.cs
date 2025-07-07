using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers
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
            var createdCategory = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(id, dto);
            if (updatedCategory == null)
                return NotFound();
            return Ok(updatedCategory);
        }
    }
}