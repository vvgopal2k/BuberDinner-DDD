using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (_userRepository.GetByEmail(query.email) is not User user)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }
            if (user.Password != query.password)
            {
                return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
