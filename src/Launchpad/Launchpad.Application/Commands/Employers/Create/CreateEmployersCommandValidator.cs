using FluentValidation;

namespace Launchpad.Application.Commands.Employers.Create;

public class CreateEmployersCommandValidator : AbstractValidator<CreateEmployersCommandRequest>
{
    public CreateEmployersCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .Length(2, 64);

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}