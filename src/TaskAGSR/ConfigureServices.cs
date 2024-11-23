using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskAGSR.Swagger;

namespace TaskAGSR;

public static class ConfigureServices
{
    private static void ConfigureJsonOptions(this JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
    }

    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services
            .Configure<JsonOptions>(v => v.SerializerOptions.ConfigureJsonOptions())
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(v =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                v.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                v.SupportNonNullableReferenceTypes();
                v.SchemaFilter<SwaggerSchemaDefaultFilter>();
                v.OperationFilter<SwaggerRequestBodyFilter>();
            });

        services.AddControllers()
            .AddJsonOptions(v => v.JsonSerializerOptions.ConfigureJsonOptions());

        return services;
    }
}
