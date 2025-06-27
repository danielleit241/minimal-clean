using MinimalApi.Abstractions;
using MinimalApi.Booststrapping;
using MinimalApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.RegisterEndpointDefinitions(typeof(Program).Assembly);


var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var endpointDefinitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();
    foreach (var endpoint in endpointDefinitions)
    {
        endpoint.RegisterEndpoints(app);
    }
}


app.Run();
