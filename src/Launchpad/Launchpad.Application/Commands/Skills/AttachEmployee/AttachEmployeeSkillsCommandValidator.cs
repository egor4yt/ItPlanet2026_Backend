using FluentValidation;

namespace Launchpad.Application.Commands.Skills.AttachEmployee;

public class AttachEmployeeSkillsCommandValidator : AbstractValidator<AttachEmployeeSkillsCommandRequest>
{
    public AttachEmployeeSkillsCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);

        RuleForEach(x => x.Skills)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Title)
                    .NotEmpty()
                    .Length(2, 64);
            });
    }
}