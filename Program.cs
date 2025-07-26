using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using swagger.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument(
    o => o.DocumentSettings = s => s.OperationProcessors.Add(new MyEndpointProcessor()));

var app = builder.Build();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
}).UseSwaggerGen();

app.Run();

