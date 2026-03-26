using FluentValidation;
using Launchpad.Application.SharedModels.Validators;

namespace Launchpad.Application.Commands.Vacancies.Create;

public class CreateVacanciesCommandValidator : AbstractValidator<CreateVacanciesCommandRequest>
{
    public CreateVacanciesCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.City)
            .NotEmpty();

        RuleFor(x => x.FullAddress)
            .NotEmpty();

        RuleFor(x => x.WorkFormatIds)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Location)
            .NotNull()
            .SetValidator(new GeolocationPointValidator());

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue);
    }
}