using Microsoft.AspNetCore.Mvc;
using Domain;
using Application;
using AutoMapper;
using FluentValidation;
using Application.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            try
            {
                var createdProduct = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
        {
            try
            {
                var success = await _productService.UpdateAsync(id, dto);
                if (success)
                    return NoContent();
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (ArgumentException)
            {
                return BadRequest("ID mismatch");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _productService.DeleteAsync(id);
            if (success)
                return NoContent();
            return NotFound();
        }
    }
}