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
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return HandleSuccess(categories, "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            try
            {
                var createdCategory = await _categoryService.CreateAsync(dto);
                return HandleCreated(createdCategory, nameof(GetAll), new { id = createdCategory.Id });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
        {
            try
            {
                var success = await _categoryService.UpdateAsync(id, dto);
                if (!success)
                    return HandleNotFoundException("Category not found");

                return HandleSuccess(null, "Category updated successfully");
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _categoryService.DeleteAsync(id);
                if (!success)
                    return HandleNotFoundException("Category not found");

                return HandleNoContent();
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }
    }
}