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

        protected async Task<IActionResult> HandleServiceCall<T>(
            Func<Task<T>> serviceCall,
            Func<T, IActionResult> successHandler,
            string notFoundMessage = null)
        {
            try
            {
                var result = await serviceCall();

                // Handle null results as not found
                if (result == null)
                {
                    return HandleNotFoundException(notFoundMessage ?? "Resource not found");
                }

                return successHandler(result);
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (ArgumentException ex)
            {
                return HandleBadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

 
        protected async Task<IActionResult> HandleServiceCall(
            Func<Task<bool>> serviceCall,
            string notFoundMessage = "Operation failed",
            string successMessage = "Operation completed successfully")
        {
            try
            {
                var success = await serviceCall();

                if (!success)
                {
                    return HandleNotFoundException(notFoundMessage);
                }

                return HandleSuccess(message: successMessage);
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (ArgumentException ex)
            {
                return HandleBadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

        protected async Task<IActionResult> HandleServiceCall(
            Func<Task> serviceCall,
            string successMessage = "Operation completed successfully")
        {
            try
            {
                await serviceCall();
                return HandleSuccess(message: successMessage);
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (ArgumentException ex)
            {
                return HandleBadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
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