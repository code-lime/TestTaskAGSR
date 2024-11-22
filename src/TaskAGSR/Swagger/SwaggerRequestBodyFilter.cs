using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace TaskAGSR.Swagger;

public class SwaggerRequestBodyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttribute<SwaggerRequestBodyAttribute>() is SwaggerRequestBodyAttribute attribute)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    [attribute.MimeType] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    }
                }
            };
        }
    }
}