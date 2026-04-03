using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationTasks = validators.Select(x => x.ValidateAsync(validationContext, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);
            var validationFailures = validationResults
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (validationFailures.Count != 0) return CreateFailureResponse(validationFailures);
        }

        return await next(cancellationToken);
    }

    private static TResponse CreateFailureResponse(IEnumerable<ValidationFailure> failures)
    {
        var responseType = typeof(TResponse);

        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<,>))
        {
            var resultValueType = responseType.GetGenericArguments()[0];
            var resultErrorType = responseType.GetGenericArguments()[1];

            if (resultErrorType == typeof(ErrorCollection))
            {
                var errors = failures.Select(x => new Error(x.PropertyName, x.ErrorMessage));
                var domainErrorCollection = new ErrorCollection(errors, ErrorCollectionType.ValidationError);

                var failureMethod = typeof(Result).GetMethods()
                    .First(m => m is { Name: nameof(Result.Failure), IsGenericMethod: true } && m.GetGenericArguments().Length == 2)
                    .MakeGenericMethod(resultValueType, resultErrorType);

                return (TResponse)failureMethod.Invoke(null, [domainErrorCollection])!;
            }
        }

        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultErrorType = responseType.GetGenericArguments()[0];
            if (resultErrorType == typeof(ErrorCollection))
            {
                var errors = failures.Select(x => new Error(x.PropertyName, x.ErrorMessage));
                var domainErrorCollection = new ErrorCollection(errors, ErrorCollectionType.ValidationError);

                var failureMethod = typeof(Result).GetMethods()
                    .First(m => m is { Name: nameof(Result.Failure), IsGenericMethod: true } && m.GetGenericArguments().Length == 1)
                    .MakeGenericMethod(resultErrorType);

                return (TResponse)failureMethod.Invoke(null, [domainErrorCollection])!;
            }
        }

        if (responseType == typeof(Result)) throw new InvalidOperationException("Specify error type");

        throw new ValidationException(failures);
    }
}