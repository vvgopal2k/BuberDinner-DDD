using BuberDinner.Api.Common.Error;
using BuberDinner.Api.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
