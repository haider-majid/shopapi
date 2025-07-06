using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;

namespace Presentation.Controllers
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is ValidationException validationException)
            {
                context.Result = new BadRequestObjectResult(validationException.Errors);
            }
            else
            {
                context.Result = new BadRequestObjectResult(new { message = exception.Message });
            }

            context.ExceptionHandled = true;
        }
    }
} 