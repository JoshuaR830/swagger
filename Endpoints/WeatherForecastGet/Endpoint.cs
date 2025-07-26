using FastEndpoints;
using Swashbuckle.AspNetCore.Filters;

namespace swagger.Endpoints.WeatherForecastGet;

public record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);

public class WeatherForecastRequest
{
    [FromQuery]
    public string Date { get; set; }
    public string Time { get; set; }
}
//[property:QueryParam]DateOnly Date);
public class Endpoint : Endpoint<WeatherForecastRequest, WeatherForecastResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/weatherforecast");
    }

    public override async Task HandleAsync(WeatherForecastRequest date, CancellationToken ct)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        // if (date.Date == new DateOnly(2020, 1, 1))
        // {
        //     var sunny = new WeatherForecastResponse(date.Date, 10, "Sunny");
        //     await SendAsync(sunny, cancellation: ct);
        //     return;
        // }
        //
        // if (date.Date == new DateOnly(2020, 1, 2))
        // {
        //     var rainy = new WeatherForecastResponse(date.Date, 1, "Rainy");
        //     await SendAsync(rainy, cancellation: ct);
        //     return;
        // }

        // var forecast =
        //     new WeatherForecastResponse
        //     (
        //         date.Date,
        //         Random.Shared.Next(-20, 55),
        //         summaries[Random.Shared.Next(summaries.Length)]
        //     ); 
        var forecast =
            new WeatherForecastResponse
            (
                new DateOnly(2022, 1, 1),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            );
        
        await SendAsync(forecast, cancellation: ct);
    }
}