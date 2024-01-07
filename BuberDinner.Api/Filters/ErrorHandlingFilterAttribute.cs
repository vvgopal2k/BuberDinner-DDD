using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.Api.Filters
{
    //approach 2, this is not need as well. 3rd approach is creating new controller to handle exception "/error" path: returns  controllerBase.Problem object
    public class ErrorHandlingFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
           var ex = context.Exception;
            //var errorResult = new { error = "An error occurred while processing your request2" };
            //client needs to know the structure of this result. So, the better approach is to use Problem Details by RFC Specification below
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "An error occurred while processing your request2.",
                Status = (int)HttpStatusCode.InternalServerError
            };
           
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}
