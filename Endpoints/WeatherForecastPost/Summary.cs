using FastEndpoints;

namespace swagger.Endpoints.WeatherForecastPost;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Get weather forecast";
        Description = "Returns a list of weather forecasts for the next 5 days.";
        Response<WeatherForecastResponse>(200, "A list of weather forecasts.");
        
        RequestExamples.Add(new RequestExample(new WeatherForecastRequest(new DateOnly(2020, 1, 1), 10, "Sunny"), "Sunny"));
        RequestExamples.Add(new RequestExample(new WeatherForecastRequest(new DateOnly(2020, 1, 2), 1, "Rainy"), "Rainy"));
        
        ResponseExamples[200] = new List<WeatherForecastResponse>()
        {
            new(new DateOnly(2020, 1, 1), 10, "Sunny"),
            new(new DateOnly(2020, 1, 2), 1, "Rainy")
        };
    }
}