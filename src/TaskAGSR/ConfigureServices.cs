using Hl7.Fhir.Search;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskAGSR.Converters;
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
                v.MapType<DateTimeSearch>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "fhir-datetime",
                    Example = new OpenApiString("eq2023-11-23T15:30:00Z")
                });
                v.OperationFilter<SwaggerRequestBodyFilter>();
            });

        TypeDescriptor.AddAttributes(typeof(DateTimeSearch), new TypeConverterAttribute(typeof(DateTimeSearchConverter)));
        services
            .AddControllers()
            .AddJsonOptions(v => v.JsonSerializerOptions.ConfigureJsonOptions());

        return services;
    }
}
