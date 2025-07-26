using FastEndpoints;

namespace swagger.Endpoints;

public interface IMultipleSummary
{
    Dictionary<int, List<OpenApiExampleInfo>> MultipleResponseExamples { get; set; }
}

public class MultipleSummary<TEndpoint> : Summary<TEndpoint>, IMultipleSummary where TEndpoint : IEndpoint
{
    public Dictionary<int, List<OpenApiExampleInfo>> MultipleResponseExamples { get; set; } = [];
}