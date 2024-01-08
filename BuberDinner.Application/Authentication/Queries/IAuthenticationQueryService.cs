using BuberDinner.Application.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}
