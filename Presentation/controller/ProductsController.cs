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
            return await HandleServiceCall(
                () => _productService.GetAllAsync(),
                products => HandleSuccess(products, "Products retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await HandleServiceCall(
                () => _productService.GetByIdAsync(id),
                product => HandleSuccess(product, "Product retrieved successfully"),
                "Product not found"
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            return await HandleServiceCall(
                () => _productService.CreateAsync(dto),
                createdProduct => HandleCreated(createdProduct, nameof(GetById), new { id = createdProduct.Id })
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
        {
            return await HandleServiceCall(
                () => _productService.UpdateAsync(id, dto),
                "Product not found",
                "Product updated successfully"
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await HandleServiceCall(
                () => _productService.DeleteAsync(id),
                "Product not found",
                "Product deleted successfully"
            );
        }
    }
}