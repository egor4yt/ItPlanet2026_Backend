using FluentValidation;

namespace Launchpad.Application.Commands.Employers.Update;

public class UpdateEmployersCommandValidator : AbstractValidator<UpdateEmployersCommandRequest>
{
    public UpdateEmployersCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(4096);
    }
}