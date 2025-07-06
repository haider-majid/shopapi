using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IActionResult HandleValidationException(ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }

        protected IActionResult HandleNotFoundException(string message = "Resource not found")
        {
            return NotFound(new { message });
        }

        protected IActionResult HandleSuccess(object data = null, string message = "Operation completed successfully")
        {
            return Ok(new { message, data });
        }

        protected IActionResult HandleCreated(object data, string actionName, object routeValues)
        {
            return CreatedAtAction(actionName, routeValues, data);
        }

        protected IActionResult HandleNoContent()
        {
            return NoContent();
        }

        protected IActionResult HandleBadRequest(string message = "Invalid request")
        {
            return BadRequest(new { message });
        }
    }
}