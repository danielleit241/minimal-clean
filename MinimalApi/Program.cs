using MinimalApi.Booststrapping;
using MinimalApi.Endpoints.PostEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPostEndpoints();

app.Run();
