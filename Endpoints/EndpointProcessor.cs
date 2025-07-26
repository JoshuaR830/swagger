using FastEndpoints.Swagger;
using Newtonsoft.Json.Linq;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace swagger.Endpoints;

public class MyEndpointProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        context.GetEndpointDefinition();

        if (context.GetEndpointDefinition()?.EndpointSummary is not IMultipleSummary summary)
        {
            return true;
        }

        var operation = context.OperationDescription.Operation;

        foreach (var response in operation.Responses)
        {
            var statusCode = int.Parse(response.Key);

            if (!summary.MultipleResponseExamples.TryGetValue(statusCode, out var examples))
                continue;

            var mediaType = response.Value.Content.FirstOrDefault().Value;

            mediaType.Example = null;


            foreach (var example in examples)
            {
                // ToDo set up Json serializer that makes this right
                example.SerializedValue ??= JToken.FromObject(example.Value);
                mediaType.Examples.Add(example.Name, new OpenApiExample
                {
                    Description = example.Description,
                    Summary = example.Summary,
                    Value = example.Value
                });
            }
        }

        return true;
    }
}