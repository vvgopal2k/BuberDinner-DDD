using BuberDinner.Application.Authentication.Commands;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    //[ErrorHandlingFilter] for all controllers add it in program file
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;

        public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
        {
            _authenticationCommandService = authenticationCommandService;
            _authenticationQueryService = authenticationQueryService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
           ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(request.FirstName,request.LastName,request.Email,request.Password);
            return authResult.Match(authResul => Ok(MapAuthResult(authResul)), errors => Problem(errors));

        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
        {
            return new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationQueryService.Login(request.Email, request.Password);
            if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }
            return authResult.Match(authResult=>Ok(MapAuthResult(authResult)), errors => Problem(errors));
        }
    }
}
