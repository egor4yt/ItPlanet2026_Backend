using FluentValidation;

namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryValidator : AbstractValidator<SearchEmployersQueryRequest>
{
    public SearchEmployersQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}