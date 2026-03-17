using FluentValidation;

namespace Launchpad.Application.Queries.Skills.Search;

public class SearchSkillsQueryValidator : AbstractValidator<SearchSkillsQueryRequest>
{
    public SearchSkillsQueryValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(64);

        RuleFor(x => x.Count)
            .GreaterThan(0)
            .LessThanOrEqualTo(50);
    }
}