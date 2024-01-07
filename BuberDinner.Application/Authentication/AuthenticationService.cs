using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
using FluentResults;

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

        public AuthenticationResult Login(string email, string password)
        {
            if(_userRepository.GetByEmail(email) is not User user)
            {
                throw new Exception("User doesnot exist");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // check if user already exists
            if (_userRepository.GetByEmail(email) is not null)
            {
                return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
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
