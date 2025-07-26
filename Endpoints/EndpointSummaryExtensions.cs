using System.Text.Json.Serialization;
using FastEndpoints;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;

namespace swagger.Endpoints;

// public static class EndpointSummaryExtensions
// {
//     public static T ResponseExamples<T>(this EndpointSummary s, int statusCode, params OpenApiExampleInfo[] examples)
//         where T : EndpointSummary
//     {
//         
//         s.Summary.EndpointMetadata
//     }
// }

public class OpenApiExampleInfo
{
    public string Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public object Value { get; set; }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    internal JToken? SerializedValue { get; set; }
}