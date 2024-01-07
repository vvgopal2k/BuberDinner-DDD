﻿using BuberDinner.Application.Common.Errors;
using FluentResults;

namespace BuberDinner.Application.Authentication
{
    public interface IAuthenticationService
    {
       Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}
