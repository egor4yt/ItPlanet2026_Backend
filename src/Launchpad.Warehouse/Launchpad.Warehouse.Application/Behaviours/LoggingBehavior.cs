using System.Diagnostics;
using System.Diagnostics.Metrics;
using Launchpad.Warehouse.Application.Metrics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Launchpad.Warehouse.Application.Behaviours;

public partial class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid();

        LogRequestStart(requestName, requestGuid);

        TResponse response;

        try
        {
            response = await next(cancellationToken);
            WarehouseMeter.MediatrHandlerExecuted.Add(1, new KeyValuePair<string, object?>("request", requestName));
        }
        finally
        {
            stopwatch.Stop();
            LogRequestEnd(requestName, requestGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Handling {RequestName} {RequestId}")]
    private partial void LogRequestStart(string requestName, Guid requestId);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Handled {RequestName} {RequestId}, Execution time={ExecutionTime} ms")]
    private partial void LogRequestEnd(string requestName, Guid requestId, long executionTime);
}