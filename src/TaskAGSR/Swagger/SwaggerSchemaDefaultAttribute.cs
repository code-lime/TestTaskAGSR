namespace TaskAGSR.Swagger;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false)]
public class SwaggerSchemaDefaultAttribute(object defaultValue) : Attribute
{
    public object DefaultValue { get; set; } = defaultValue;
}
