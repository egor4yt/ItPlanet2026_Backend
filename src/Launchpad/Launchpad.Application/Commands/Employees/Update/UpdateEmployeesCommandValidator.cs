using FluentValidation;

namespace Launchpad.Application.Commands.Employees.Update;

public class UpdateEmployeesCommandValidator : AbstractValidator<UpdateEmployeesCommandRequest>
{
    public UpdateEmployeesCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .Length(2, 64);

        RuleFor(x => x.LastName)
            .Length(2, 64);

        RuleFor(x => x.MiddleName)
            .Length(2, 64);

        RuleFor(x => x.BirthDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-100)))
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-12)));
    }
}