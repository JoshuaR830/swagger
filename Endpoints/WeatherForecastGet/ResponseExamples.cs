using Swashbuckle.AspNetCore.Filters;

namespace swagger.Endpoints.WeatherForecastGet;

public class ResponseExamples : IMultipleExamplesProvider<WeatherForecastResponse>
{
    public IEnumerable<SwaggerExample<WeatherForecastResponse>> GetExamples() =>
        new List<SwaggerExample<WeatherForecastResponse>>
        {
            new()
            {
                Name = "Rainy",
                Value = new WeatherForecastResponse(new DateOnly(2020, 1, 2), 1, "Rainy")
            },
            new()
            {
                Name = "Sunny",
                Value = new WeatherForecastResponse(new DateOnly(2020, 1, 1), 10, "Sunny")
            }
        };
}
//
// public class WeatherRequestExample : IMultipleExamplesProvider<WeatherForecastRequest>
// {
//     public IEnumerable<SwaggerExample<WeatherForecastRequest>> GetExamples() =>
//         new List<SwaggerExample<WeatherForecastRequest>>
//         {
//             new()
//             {
//                 Name = "Rainy",
//                 Value = new WeatherForecastRequest(new DateOnly(2020, 1, 2))
//             },
//             new()
//             {
//                 Name = "Sunny",
//                 Value = new WeatherForecastRequest(new DateOnly(2020, 1, 1))
//             }
//         };
// }