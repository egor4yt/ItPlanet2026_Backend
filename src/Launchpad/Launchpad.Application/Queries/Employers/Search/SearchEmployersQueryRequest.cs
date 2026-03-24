using Launchpad.Application.Abstrcations;
using MediatR;

namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryRequest : IRequest<PagedResult<SearchEmployersQueryResponse>>, IPaging
{
    public string? Name { get; set; }
    public int? VerificationStatusId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}