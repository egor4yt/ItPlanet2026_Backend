using FluentValidation;
using Launchpad.Application.SharedModels.Validators;

namespace Launchpad.Application.Queries.Vacancies.Search;

public class SearchVacanciesQueryValidator : AbstractValidator<SearchVacanciesQueryRequest>
{
    public SearchVacanciesQueryValidator()
    {
        Include(new PagingRequestValidator());

        RuleFor(x => x.RadiusSearch)
            .SetValidator(new GeolocationRadiusQueryValidator()!)
            .When(x => x.RadiusSearch != null);

        RuleFor(x => x.BoxSearch)
            .SetValidator(new GeolocationBoxQueryValidator()!)
            .When(x => x.BoxSearch != null);

        RuleFor(x => x)
            .Must(x => !(x.RadiusSearch != null && x.BoxSearch != null))
            .WithMessage("You must provide a radius or a box, not together");
    }
}