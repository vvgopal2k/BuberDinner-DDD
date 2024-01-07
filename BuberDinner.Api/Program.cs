using BuberDinner.Api.Filters;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  //  builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>()); //The approach is to redirect to error handling controller: controllerBase.Problem obect
    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
}
var app = builder.Build();
{

    //app.UseMiddleware<ErrorHandlingMiddleware>(); // Global error handling using middleware. Alternatively use Exception filter
    app.UseExceptionHandler("/error"); 
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

