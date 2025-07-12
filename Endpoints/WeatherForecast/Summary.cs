using FastEndpoints;

namespace swagger.Endpoints;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Get weather forecast";
        Description = "Returns a list of weather forecasts for the next 5 days.";
        Response<WeatherForecastResponse>(200, "A list of weather forecasts.");

        ResponseExamples[200] = new List<WeatherForecastResponse>()
        {
            new WeatherForecastResponse(new DateOnly(2020, 1, 1), 10, "Sunny"),
            new WeatherForecastResponse(new DateOnly(2020, 1, 1), 1, "Rainy")
        };
    }
}