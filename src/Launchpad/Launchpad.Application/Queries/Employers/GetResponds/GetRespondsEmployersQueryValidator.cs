using FluentValidation;
using Launchpad.Application.SharedModels.Validators;

namespace Launchpad.Application.Queries.Employers.GetResponds;

public class GetRespondsEmployersQueryValidator : AbstractValidator<GetRespondsEmployersQueryRequest>
{
    public GetRespondsEmployersQueryValidator()
    {
        Include(new PagingRequestValidator());
    }
}