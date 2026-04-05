using System.Diagnostics;
using System.Diagnostics.Metrics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Launchpad.Application.Behaviours;

public partial class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private static readonly Meter Meter = new Meter("Launchpad.Candidates", "1.0.0"); 
    private static readonly Counter<int> MediatrHandlerExecuted = Meter.CreateCounter<int>("mediatr_handler_executed");
    
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
            MediatrHandlerExecuted.Add(1, new KeyValuePair<string, object?>("request", requestName));
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