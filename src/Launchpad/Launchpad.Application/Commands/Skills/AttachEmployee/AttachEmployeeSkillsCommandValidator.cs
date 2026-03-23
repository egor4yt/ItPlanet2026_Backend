using FluentValidation;

namespace Launchpad.Application.Commands.Skills.AttachEmployee;

public class AttachEmployeeSkillsCommandValidator : AbstractValidator<AttachEmployeeSkillsCommandRequest>
{
    public AttachEmployeeSkillsCommandValidator()
    {
        RuleForEach(x => x.Skills)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Title)
                    .NotEmpty()
                    .Length(1, 64);
            });
    }
}