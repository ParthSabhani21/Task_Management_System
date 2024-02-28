using Serilog.Context;
using System.Security.Claims;

namespace TaskManagement.API.Configuration;

public class LoggingMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly IHttpContextAccessor _contextAccessor;
    //private readonly ILogger<LoggingMiddleware> _logger;


    public LoggingMiddleware(RequestDelegate requestDelegate, IHttpContextAccessor contextAccessor/*, ILogger<LoggingMiddleware> logger*/)
    {
        _requestDelegate = requestDelegate;
        _contextAccessor = contextAccessor;
        //_logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logUser = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? "System";

        LogContext.PushProperty("UserName", logUser);
        LogContext.PushProperty("MethodType", context.Request.Method);
        LogContext.PushProperty("MethodPath", context.Request.Path);

        await _requestDelegate(context);

        //try
        //{
        //    // Log the request information with structured logging
        //    _logger.LogInformation("RequestInformation {Method} {Path} {User}", context.Request.Method, context.Request.Path, context.User.Identity.Name);

        //    // Call the next middleware in the pipeline
        //    await _(context);

        //    // Log the response information with structured logging
        //    _logger.LogInformation("ResponseInformation {StatusCode}", context.Response.StatusCode);
        //}
        //catch (Exception ex)
        //{
        //    // Log the exception and include the username if available
        //    _logger.LogError(ex, "An unhandled exception occurred. User: {User}", context.User.Identity.Name);

        //    // Re-throw the exception to let the global exception handler handle it
        //    throw;
        //}

    }

}
