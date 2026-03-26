using FluentValidation;
using Launchpad.Application.SharedModels.Validators;

namespace Launchpad.Application.Queries.Employees.GetResponds;

public class GetRespondsEmployeesQueryValidator : AbstractValidator<GetRespondsEmployeesQueryRequest>
{
    public GetRespondsEmployeesQueryValidator()
    {
        Include(new PagingRequestValidator());
    }
}