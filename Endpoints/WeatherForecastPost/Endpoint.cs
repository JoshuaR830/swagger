using FastEndpoints;

namespace swagger.Endpoints.WeatherForecastPost;

public record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);
public record WeatherForecastRequest(DateOnly Date, int TemperatureC, string? Summary);
public class Endpoint : Endpoint<WeatherForecastRequest, WeatherForecastResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("/weatherforecast");
    }

    public override async Task HandleAsync(WeatherForecastRequest request, CancellationToken ct)
    {
        var forecast =
            new WeatherForecastResponse
            (
                request.Date,
                request.TemperatureC,
                request.Summary
            );
        
        await SendAsync(forecast, cancellation: ct);
    }
}