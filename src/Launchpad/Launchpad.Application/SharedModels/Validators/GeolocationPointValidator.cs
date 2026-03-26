using FluentValidation;

namespace Launchpad.Application.SharedModels.Validators;

public class GeolocationPointValidator : AbstractValidator<GeolocationPoint>
{
    public GeolocationPointValidator()
    {
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90);
    }
}