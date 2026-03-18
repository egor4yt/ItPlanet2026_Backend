using FluentValidation;

namespace Launchpad.Application.Commands.Employees.UpdateBiography;

public class UpdateBiographyEmployeesCommandValidator : AbstractValidator<UpdateBiographyEmployeesCommandRequest>
{
    public UpdateBiographyEmployeesCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);

        RuleFor(x => x.Biography)
            .MaximumLength(4096);
    }
}