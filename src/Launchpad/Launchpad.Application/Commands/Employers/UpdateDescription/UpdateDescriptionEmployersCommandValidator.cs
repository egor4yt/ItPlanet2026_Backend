using FluentValidation;

namespace Launchpad.Application.Commands.Employers.UpdateDescription;

public class UpdateDescriptionEmployersCommandValidator : AbstractValidator<UpdateDescriptionEmployersCommandRequest>
{
    public UpdateDescriptionEmployersCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(4096);
    }
}