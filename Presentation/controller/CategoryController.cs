using Presentation.Dto.Category;
using Presentation.Dto.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands.Category;

namespace Presentation.Controllers;

public class CategoryController : BaseController
{
    private readonly IMediator _mediator;

    public CategoryController(IMapper mapper, IMediator mediator) : base(mapper)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var createdCategory = await _mediator.Send(new CreateCategoryCommand(dto));
        return CreatedAtAction(nameof(GetAll), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
    {
        var updatedCategory = await _mediator.Send(new UpdateCategoryCommand(id, dto));
        if (updatedCategory == null)
            return NotFound();
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        if (!result)
            return NotFound();
        return NoContent();
    }
}
