using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            if(_userRepository.GetByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            if (user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // check if user already exists
            if (_userRepository.GetByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }


            var user = new User { FirstName= firstName, LastName= lastName,  Email = email, Password = password };
            //Create user (generate unique id)
            _userRepository.Add(user);
            //create token
           
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user,  token);
        }
    }
}
