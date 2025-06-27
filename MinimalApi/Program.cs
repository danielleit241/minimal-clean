using MinimalApi.Abstractions;
using MinimalApi.Booststrapping;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.RegisterEndpointDefinitions(typeof(Program).Assembly);


var app = builder.Build();

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var endpointDefinitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();
    foreach (var endpoint in endpointDefinitions)
    {
        endpoint.RegisterEndpoints(app);
    }
}


app.Run();
