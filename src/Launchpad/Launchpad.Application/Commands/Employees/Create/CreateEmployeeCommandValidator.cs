using FluentValidation;

namespace Launchpad.Application.Commands.Employees.Create;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommandRequest>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(2, 64);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 64);

        RuleFor(x => x.MiddleName)
            .MaximumLength(64);

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}