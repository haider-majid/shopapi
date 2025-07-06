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
            return await HandleServiceCall(
                () => _categoryService.GetAllAsync(),
                categories => HandleSuccess(categories, "Categories retrieved successfully")
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            return await HandleServiceCall(
                () => _categoryService.CreateAsync(dto),
                createdCategory => HandleCreated(createdCategory, nameof(GetAll), new { id = createdCategory.Id })
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
        {
            return await HandleServiceCall(
                () => _categoryService.UpdateAsync(id, dto),
                "Category not found",
                "Category updated successfully"
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await HandleServiceCall(
                () => _categoryService.DeleteAsync(id),
                "Category not found",
                "Category deleted successfully"
            );
        }
    }
}