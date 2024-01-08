using BuberDinner.Api.Common.Error;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}
var app = builder.Build();
{
    app.UseExceptionHandler("/error"); 
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

