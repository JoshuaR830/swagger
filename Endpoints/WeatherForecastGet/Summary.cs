using FastEndpoints;

namespace swagger.Endpoints.WeatherForecastGet;

public class Summary : MultipleSummary<Endpoint>
{
    public Summary()
    {
        Summary = "Get weather forecast";
        Description = "Returns a list of weather forecasts for the next 5 days.";
        Response<WeatherForecastResponse>(200, "A list of weather forecasts.");
        
        RequestExamples.Add(new RequestExample(new DateOnly(2020, 1, 1)));
        
        MultipleResponseExamples[200] = new List<OpenApiExampleInfo>()
        {
            new OpenApiExampleInfo
            {
                Name = "Sunny Example",
                Value = new WeatherForecastResponse(new DateOnly(2020, 1, 1), 10, "Sunny")
            },
            new OpenApiExampleInfo
            {
                Name = "Rainy Example",
                Value = new WeatherForecastResponse(new DateOnly(2020, 1, 1), 1, "Rainy")
            }
        };
    }
}