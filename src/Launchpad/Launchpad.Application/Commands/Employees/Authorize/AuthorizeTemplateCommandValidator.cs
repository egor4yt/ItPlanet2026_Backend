using FluentValidation;

namespace Launchpad.Application.Commands.Employees.Authorize;

public class AuthorizeEmployeeCommandValidator : AbstractValidator<AuthorizeEmployeeCommandRequest>
{
    public AuthorizeEmployeeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();
    }
}