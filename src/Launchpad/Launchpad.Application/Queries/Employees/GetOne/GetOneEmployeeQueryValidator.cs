using FluentValidation;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeeQueryValidator : AbstractValidator<GetOneEmployeeQueryRequest>
{
    public GetOneEmployeeQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}