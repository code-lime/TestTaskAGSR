using Microsoft.AspNetCore.Http.Json;
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
                v.SupportNonNullableReferenceTypes();
                v.SchemaFilter<SwaggerSchemaDefaultFilter>();
                v.OperationFilter<SwaggerRequestBodyFilter>();
            });

        services.AddControllers()
            .AddJsonOptions(v => v.JsonSerializerOptions.ConfigureJsonOptions());

        return services;
    }
}
