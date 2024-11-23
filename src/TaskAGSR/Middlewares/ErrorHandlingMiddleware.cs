using Serilog;
using TaskAGSR.Models.Response;

namespace TaskAGSR.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger logger)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            logger.Error(e, "InternalServerError");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(ErrorResponse.Create(e.Message));
        }
    }
}
