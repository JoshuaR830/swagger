using Swashbuckle.AspNetCore.Filters;

namespace swagger.Endpoints.WeatherForecastPost;

public class ResponseExamples : IMultipleExamplesProvider<WeatherForecastPostResponse>
{
    public IEnumerable<SwaggerExample<WeatherForecastPostResponse>> GetExamples() =>
        new List<SwaggerExample<WeatherForecastPostResponse>>
        {
            new()
            {
                Name = "Rainy",
                Value = new WeatherForecastPostResponse(new DateOnly(2020, 1, 2), 1, "Rainy")
            },
            new()
            {
                Name = "Sunny",
                Value = new WeatherForecastPostResponse(new DateOnly(2020, 1, 1), 10, "Sunny")
            }
        };
}

public class RequestExamples : IMultipleExamplesProvider<WeatherForecastPostRequest>
{
    public IEnumerable<SwaggerExample<WeatherForecastPostRequest>> GetExamples() =>
        new List<SwaggerExample<WeatherForecastPostRequest>>
        {
            new()
            {
                Name = "Rainy",
                Value = new WeatherForecastPostRequest(new DateOnly(2020, 1, 2), 1, "Rainy")
            },
            new()
            {
                Name = "Sunny",
                Value = new WeatherForecastPostRequest(new DateOnly(2020, 1, 1), 10, "Sunny")
            }
        };
}