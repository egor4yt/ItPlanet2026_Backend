using FluentValidation;

namespace Launchpad.Application.Commands.EmployeeProjects.Create;

public class CreateEmployeeProjectsCommandValidator : AbstractValidator<CreateEmployeeProjectsCommandRequest>
{
    public CreateEmployeeProjectsCommandValidator()
    {
        RuleFor(x => x.Title)
            .Length(1, 64);

        RuleFor(x => x.Description)
            .Length(1, 4096);

        RuleFor(x => x.Link)
            .Length(1, 256);

        RuleFor(x => x.Specialization)
            .Length(1, 64);
    }
}