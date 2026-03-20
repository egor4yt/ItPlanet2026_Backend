using FluentValidation;

namespace Launchpad.Application.Commands.EmployerVerifications.Update;

public class UpdateEmployerVerificationsCommandValidator : AbstractValidator<UpdateEmployerVerificationsCommandRequest>
{
    public UpdateEmployerVerificationsCommandValidator()
    {
        RuleFor(x => x.RequestMessage)
            .Length(2, 1024);

        RuleFor(x => x.TaxpayerIndividualNumber)
            .Must(x => x is { Length: 10 or 12 })
            .WithMessage("Length must be 10 or 12 characters");
    }
}