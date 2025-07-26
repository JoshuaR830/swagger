// Filters/FromQueryOperationFilter.cs
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using FastEndpoints; // For PropertyInfo

public class FromQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if the endpoint uses a request DTO (FastEndpoints typically does)
        var requestType = context.MethodInfo.GetParameters()
            .FirstOrDefault(p => p.ParameterType.BaseType != null)?
            .ParameterType;

        var isGet = context.ApiDescription.HttpMethod?.ToLowerInvariant() == "get";

        if (requestType == null || !isGet)
            return;

        var parameters = new List<OpenApiParameter>();

        var properties = operation.RequestBody.Content.Select(c => c.Value.Schema).Where(s => s.Properties != null).Select(x => x.Properties);
        foreach (var property in properties)
        {
            parameters.AddRange(property.Select(x => new OpenApiParameter
            {
                 Name = x.Key,
                 Schema = x.Value
            }));
        }
        
        // Iterate through properties of the request DTO
        foreach (var property in requestType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // Check if the property has the [FromQuery] attribute
            // var fromQueryAttribute = property.GetCustomAttribute<FromQueryAttribute>();

            // For GET/HEAD methods, FastEndpoints defaults to FromQuery if not specified,
            // but [FromQuery] is more explicit.
            // bool isQueryParam = fromQueryAttribute != null ||
            //                     (context.ApiDescription.HttpMethod == HttpMethod.Get.ToString()?.ToUpperInvariant() &&
            //                      !property.GetCustomAttributes().Any(attr => attr is FromRouteAttribute || attr is FromBodyAttribute));

            // if (fromQueryAttribute != null)
            // {
                // Remove the property from the requestBody schema if it exists there
                // This ensures it doesn't appear as a body parameter
                
                
            // There should never be a request body for a get request
            // parameters.Add(
            //
            //     new OpenApiParameter
            //     {
            //         Name = property.Name
            //     }
            // );

            operation.RequestBody = null;

            if (operation.RequestBody != null && operation.RequestBody.Content.Any())
            {
                foreach (var content in operation.RequestBody.Content.Values)
                {
                    if (content.Schema != null && content.Schema.Properties != null)
                    {
                        content.Schema.Properties.Remove(property.Name.ToLowerCamelCase()); // FastEndpoints default JSON casing
                    }
                }
                // If no more properties are in the body, remove the requestBody entirely
                if (operation.RequestBody.Content.All(c => c.Value.Schema?.Properties?.Any() != true))
                {
                    operation.RequestBody = null;
                }
                // }

                // Add or update the parameter in the operation's parameters list
                var existingParam = operation.Parameters.FirstOrDefault(p => p.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));

                if (existingParam != null)
                {
                    // Update existing parameter if it's not already 'in: query'
                    if (existingParam.In != ParameterLocation.Query)
                    {
                        existingParam.In = ParameterLocation.Query;
                        existingParam.Required = !property.PropertyType.IsNullable(); // Set required based on nullability
                        // Set schema type based on property type (simplified for brevity)
                        existingParam.Schema = new OpenApiSchema { Type = GetOpenApiSchemaType(property.PropertyType) };
                    }
                }
                else
                {
                    // Add new query parameter
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = property.Name.ToLowerCamelCase(), // FastEndpoints often uses camelCase for QSP
                        In = ParameterLocation.Query,
                        Required = !property.PropertyType.IsNullable(),
                        Schema = new OpenApiSchema { Type = GetOpenApiSchemaType(property.PropertyType) },
                        Description = property.Name // Or get from XML comments if enabled
                    });
                }
            }
            
            operation.Parameters = parameters;
        }
    }

    // Helper to get OpenAPI schema type (simplified, expand for more types like DateOnly, Guid etc.)
    private string GetOpenApiSchemaType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        if (type == typeof(int) || type == typeof(long)) return "integer";
        if (type == typeof(float) || type == typeof(double) || type == typeof(decimal)) return "number";
        if (type == typeof(bool)) return "boolean";
        if (type == typeof(DateOnly)) return "string"; // Handled by DateOnlySchemaFilter typically, but good to set here too
        return "string"; // Default
    }
}

// Extension for nullability check (can be defined elsewhere)
public static class TypeExtensions
{
    public static bool IsNullable(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null || !type.IsValueType;
    }
}

// Extension for camel casing (FastEndpoints default)
public static class StringExtensions
{
    public static string ToLowerCamelCase(this string s)
    {
        if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
        {
            return s;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToLowerInvariant(a[0]);
        return new string(a);
    }
}