using FastEndpoints;

namespace swagger.Endpoints.WeatherForecastGet;

public record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);
public class Endpoint : Endpoint<DateOnly, WeatherForecastResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/weatherforecast");
        
    }

    public override async Task HandleAsync(DateOnly date, CancellationToken ct)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast =
            new WeatherForecastResponse
            (
                DateOnly.FromDateTime(DateTime.Now),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            );
        
        await SendAsync(forecast, cancellation: ct);
    }
}