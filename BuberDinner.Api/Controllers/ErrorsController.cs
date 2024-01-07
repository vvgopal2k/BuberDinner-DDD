using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {

    [Route("/error")]
        public IActionResult Error()
        {
            //to get the exception thrown
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            //This switch may not be a good idea, doesn't scale well. So, create a ServiceException interface...
            //var (statusCode, message) = exception switch
            //{
            //    DuplicateEmailException => (StatusCodes.Status409Conflict, "Email already exists."),
            //    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            //};
            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            return Problem(title: message, statusCode: statusCode);
        }
    }
}
