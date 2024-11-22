namespace TaskAGSR.Swagger;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SwaggerRequestBodyAttribute(string mimeType) : Attribute
{
    public string MimeType { get; set; } = mimeType;

    public SwaggerRequestBodyAttribute(string mimeTypeOrExtension, bool isExtension)
        : this(isExtension ? MimeTypes.GetMimeType(mimeTypeOrExtension) : mimeTypeOrExtension) { }
}
