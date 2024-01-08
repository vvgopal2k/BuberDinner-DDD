using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // check if user already exists
            if (_userRepository.GetByEmail(command.email) is not null)
            {
                return Domain.Common.Errors.Errors.User.DuplicateEmail;
            }


            var user = new User { FirstName = command.firstName, LastName = command.lastName, Email = command.email, Password = command.password };
            //Create user (generate unique id)
            _userRepository.Add(user);
            //create token

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
