using FluentValidation;

namespace Launchpad.Application.SharedModels.Validators;

public class GeolocationRadiusQueryValidator : AbstractValidator<GeolocationRadiusQuery>
{
    public GeolocationRadiusQueryValidator()
    {
        RuleFor(x => x.Point)
            .SetValidator(new GeolocationPointValidator());

        RuleFor(x => x.RadiusInMeters)
            .InclusiveBetween(100, 1000 * 50);
    }
}