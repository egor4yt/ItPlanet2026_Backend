using FluentValidation;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeesQueryValidator : AbstractValidator<GetOneEmployeesQueryRequest>
{
    public GetOneEmployeesQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}