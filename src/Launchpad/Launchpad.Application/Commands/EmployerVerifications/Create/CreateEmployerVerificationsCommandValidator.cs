using FluentValidation;

namespace Launchpad.Application.Commands.EmployerVerifications.Create;

public class CreateEmployerVerificationsCommandValidator : AbstractValidator<CreateEmployerVerificationsCommandRequest>
{
    public CreateEmployerVerificationsCommandValidator()
    {
        RuleFor(x => x.RequestMessage)
            .Length(2, 1024);

        RuleFor(x => x.TaxpayerIndividualNumber)
            .Must(x => x.Length is 10 or 12)
            .WithMessage("Length must be 10 or 12 characters");
    }
}