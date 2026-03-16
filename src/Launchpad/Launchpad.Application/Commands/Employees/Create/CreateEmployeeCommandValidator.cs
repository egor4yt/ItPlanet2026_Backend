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
            .MaximumLength(64)
            .MinimumLength(2);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(64)
            .MinimumLength(2);

        RuleFor(x => x.MiddleName)
            .MaximumLength(64);

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}