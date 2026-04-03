using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Candidates.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<TSuccess, TError>(this Result<TSuccess, TError> result, int successCode) where TError : ErrorCollection
    {
        if (result.IsSuccess)
            return new ObjectResult(result.Value)
            {
                StatusCode = successCode
            };

        var problem = new ProblemDetails();
        problem.Detail = string.Join(";\n", result.Error.Errors.Select(x => x.Code));
        switch (result.Error.ErrorCollectionType)
        {
            case ErrorCollectionType.InvalidOperation:
                problem.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problem.Title = "An error occurred while processing your request";
                problem.Status = StatusCodes.Status400BadRequest;
                break;
            case ErrorCollectionType.ResourceNotFound:
                problem.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                problem.Title = "The specified resource was not found";
                problem.Status = StatusCodes.Status404NotFound;
                break;
            case ErrorCollectionType.Conflict:
                problem.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
                problem.Title = "The specified resource was already exists";
                problem.Status = StatusCodes.Status409Conflict;
                break;
            case ErrorCollectionType.Unathorized:
                problem.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problem.Title = "Unathorized";
                problem.Status = StatusCodes.Status401Unauthorized;
                break;
            case ErrorCollectionType.Forbidden:
                problem.Type = "https://tools.ietf.org/html/rfc7235#section-3.1";
                problem.Title = "Forbidden";
                problem.Status = StatusCodes.Status403Forbidden;
                break;
            case ErrorCollectionType.ValidationError:
                var validationErrors = result.Error.Errors
                    .GroupBy(x => x.Code)
                    .ToDictionary(x => x.Key, x => x.Select(y => y.Message).ToArray());

                return new ObjectResult(new ValidationProblemDetails(validationErrors))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        return new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };
    }
}