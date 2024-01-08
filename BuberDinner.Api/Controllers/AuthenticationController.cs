using BuberDinner.Application.Authentication.Commands;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    //[ErrorHandlingFilter] for all controllers add it in program file
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);

           ErrorOr<AuthenticationResult> authResult =  await _mediator.Send(command);
            return authResult.Match(authResul => Ok(_mapper.Map<AuthenticationResponse>(authResul)), errors => Problem(errors));

        }

      
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var authResult = await _mediator.Send(query);

            if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description);
            }
            return authResult.Match(authResult=>Ok(_mapper.Map<AuthenticationResponse>(authResult)), errors => Problem(errors));
        }
    }
}
