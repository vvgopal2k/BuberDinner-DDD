using BuberDinner.Api;
using BuberDinner.Api.Common.Error;
using BuberDinner.Api.Common.Mapping;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{

 
    builder.Services.AddApplication();
    builder.Services.AddPresentation();
    builder.Services.AddInfrastructure(builder.Configuration);
    
}
var app = builder.Build();
{
    app.UseExceptionHandler("/error"); 
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

