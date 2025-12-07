using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Application.Commands.Product;
using Application.Queries.Product;
using Presentation.Dto.Product;

namespace Presentation.Controllers;

public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMapper mapper, IMediator mediator) : base(mapper)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var createdProduct = await _mediator.Send(new CreateProductCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
    {
        var updatedProduct = await _mediator.Send(new UpdateProductCommand(id, dto));
        if (updatedProduct == null)
            return NotFound();
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        if (!result)
            return NotFound();
        return NoContent();
    }
}
