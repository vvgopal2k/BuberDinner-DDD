using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public record LoginQuery(string email, string password) : IRequest<ErrorOr<AuthenticationResult>>;
}
