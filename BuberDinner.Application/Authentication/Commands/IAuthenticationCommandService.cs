﻿using BuberDinner.Application.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    }
}