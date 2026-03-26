using Launchpad.Application.Abstractions;
using MediatR;

namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryRequest : IRequest<PagedResult<SearchEmployersQueryResponse>>, IPagingRequest
{
    public string? Name { get; set; }
    public List<int> VerificationStatusId { get; set; } = null!;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}