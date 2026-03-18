using FluentValidation;

namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryValidator : AbstractValidator<GetOneEmployersQueryRequest>
{
    public GetOneEmployersQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}