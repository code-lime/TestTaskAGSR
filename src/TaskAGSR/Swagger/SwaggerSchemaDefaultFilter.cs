using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json;

namespace TaskAGSR.Swagger;

public class SwaggerSchemaDefaultFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo?.GetCustomAttribute<SwaggerSchemaDefaultAttribute>() is SwaggerSchemaDefaultAttribute attribute)
        {
            IOpenApiAny value = OpenApiAnyFactory.CreateFromJson(JsonSerializer.Serialize(attribute.DefaultValue));
            schema.Default = value;
        }
    }
}
