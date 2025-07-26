using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Options;
using swagger.Endpoints.WeatherForecastPost;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters(); 
    options.OperationFilter<FromQueryOperationFilter>();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<ResponseExamples>();

var app = builder.Build();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// app.UseSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    // options.RoutePrefix = string.Empty;
});

app.Run();
