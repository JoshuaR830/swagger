using FastEndpoints;

namespace swagger.Endpoints.WeatherForecastPost;


public record WeatherForecastPostResponse(DateOnly Date, int TemperatureC, string? Summary);
public record WeatherForecastPostRequest(DateOnly Date, int TemperatureC, string? Summary);

// [SwaggerResponseExample(200, typeof(SunnyExample))]
// [SwaggerResponseExample(200, typeof(Examples))]
public class Endpoint : Endpoint<WeatherForecastPostRequest, WeatherForecastPostResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("/weatherforecast");
    }

    public override async Task HandleAsync(WeatherForecastPostRequest postRequest, CancellationToken ct)
    {
        var forecast =
            new WeatherForecastPostResponse
            (
                postRequest.Date,
                postRequest.TemperatureC,
                postRequest.Summary
            );
        
        await SendAsync(forecast, cancellation: ct);
    }
}