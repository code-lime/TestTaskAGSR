using TaskAGSR.Swagger;

namespace TaskAGSR.Models.Response;

public class ErrorResponse : Response
{
    public required string Error { get; set; }
    [SwaggerSchemaDefault("error")]
    public override string Status => "error";

    public static ErrorResponse Create(string message)
        => new ErrorResponse
        {
            Error = message,
        };
}