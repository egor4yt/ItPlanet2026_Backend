using FluentValidation;
using Launchpad.Application.Abstractions;

namespace Launchpad.Application.SharedModels.Validators;

public class PagingRequestValidator : AbstractValidator<IPagingRequest>
{
    public PagingRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}