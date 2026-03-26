using FluentValidation;
using Launchpad.Application.SharedModels.Validators;

namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryValidator : AbstractValidator<SearchEmployersQueryRequest>
{
    public SearchEmployersQueryValidator()
    {
        Include(new PagingRequestValidator());
    }
}