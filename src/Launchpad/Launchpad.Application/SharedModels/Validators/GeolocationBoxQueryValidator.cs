using FluentValidation;

namespace Launchpad.Application.SharedModels.Validators;

public class GeolocationBoxQueryValidator : AbstractValidator<GeolocationBoxQuery>
{
    public GeolocationBoxQueryValidator()
    {
        RuleFor(x => x.From)
            .SetValidator(new GeolocationPointValidator());

        RuleFor(x => x.To)
            .SetValidator(new GeolocationPointValidator());
    }
}