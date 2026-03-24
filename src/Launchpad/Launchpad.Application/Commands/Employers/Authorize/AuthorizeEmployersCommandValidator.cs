using FluentValidation;

namespace Launchpad.Application.Commands.Employers.Authorize;

public class AuthorizeEmployersCommandValidator : AbstractValidator<AuthorizeEmployersCommandRequest>
{
    public AuthorizeEmployersCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();
    }
}