using Microsoft.AspNetCore.Mvc;
using Application.Services;
using AutoMapper;
using FluentValidation;
using Application;

namespace Presentation.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService) : base(mapper)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return HandleSuccess(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return HandleNotFoundException("Product not found");

                return HandleSuccess(product, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            try
            {
                var createdProduct = await _productService.CreateAsync(dto);
                return HandleCreated(createdProduct, nameof(GetById), new { id = createdProduct.Id });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
        {
            try
            {
                var success = await _productService.UpdateAsync(id, dto);
                if (!success)
                    return HandleNotFoundException("Product not found");

                return HandleNoContent();
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (ArgumentException)
            {
                return HandleBadRequest("ID mismatch");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _productService.DeleteAsync(id);
                if (!success)
                    return HandleNotFoundException("Product not found");

                return HandleNoContent();
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }
    }
}