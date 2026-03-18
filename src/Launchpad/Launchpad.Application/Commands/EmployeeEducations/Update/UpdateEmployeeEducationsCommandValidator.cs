using FluentValidation;

namespace Launchpad.Application.Commands.EmployeeEducations.Update;

public class UpdateEmployeeEducationsCommandValidator : AbstractValidator<UpdateEmployeeEducationsCommandRequest>
{
    public UpdateEmployeeEducationsCommandValidator()
    {
        RuleFor(x => x.EducationLevelId)
            .GreaterThan(0);

        RuleFor(x => x.CompletionYear)
            .GreaterThan(DateTime.UtcNow.Year - 80)
            .LessThan(DateTime.UtcNow.Year + 10);

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .Length(2, 64);

        RuleFor(x => x.Organization)
            .NotEmpty()
            .Length(2, 64);

        RuleFor(x => x.Faculty)
            .NotEmpty()
            .Length(2, 64);
    }
}