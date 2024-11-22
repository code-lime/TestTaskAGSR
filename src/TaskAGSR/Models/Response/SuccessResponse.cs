using TaskAGSR.Swagger;

namespace TaskAGSR.Models.Response;

public class SuccessResponse : Response
{
    private static readonly SuccessResponse DefaultResponse = new SuccessResponse();

    [SwaggerSchemaDefault("success")]
    public override string Status => "success";

    public static SuccessResponse Create()
        => DefaultResponse;

    public static SuccessResponse<T> Create<T>(T value)
        => new SuccessResponse<T>
        {
            Response = value,
        };
}
public class SuccessResponse<T> : SuccessResponse
{
    public required T Response { get; set; }
}
