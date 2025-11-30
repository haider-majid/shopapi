using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Application.Commands.Brand;
using Application.Queries.Brand;
using Presentation.Dto.Brand;

namespace Presentation.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IMediator _mediator;
        

        public BrandController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery(id));
            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandDto dto)
        {
            var createdBrand = await _mediator.Send(new CreateBrandCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = createdBrand.Id }, createdBrand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBrandDto dto)
        {
            var updated = await _mediator.Send(new UpdateBrandCommand(id, dto));
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteBrandCommand(id));
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
