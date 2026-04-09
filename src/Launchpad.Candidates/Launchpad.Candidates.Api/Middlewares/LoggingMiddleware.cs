using System.Diagnostics;
using Serilog.Context;

namespace Launchpad.Candidates.Api.Middlewares;

/// <summary>
///     Middleware responsible for logging contextual details of incoming HTTP requests.
/// </summary>
/// <remarks>
///     This middleware captures and enriches log data with the following properties:
///     <list type="bullet">
///         <item>TraceId: A unique identifier for the request derived from the current activity's trace or the HTTP context.</item>
///         <item>RequestMethod: The HTTP method of the incoming request (e.g., GET, POST).</item>
///         <item>RequestPath: The requested path of the HTTP endpoint.</item>
///     </list>
///     It uses the Serilog logging library to push these properties into the logging context, which enhances log traceability and
///     debugging capabilities.
/// </remarks>
/// <seealso cref="Launchpad.Candidates.Api.Configuration.DependencyInjection.UseRequestLogging" />
public class LoggingMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Middleware method that processes an incoming HTTP request by injecting contextual logging properties
    ///     such as TraceId, RequestMethod, and RequestPath into the logging pipeline,
    ///     and then passes the request to the next middleware in the pipeline.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task that represents the asynchronous operation of the middleware.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

        using (LogContext.PushProperty("TraceId", traceId))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        {
            await next(context);
        }
    }
}