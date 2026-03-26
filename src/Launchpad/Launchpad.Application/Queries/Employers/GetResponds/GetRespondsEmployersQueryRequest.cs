using Launchpad.Application.Abstractions;
using MediatR;

namespace Launchpad.Application.Queries.Employers.GetResponds;

public class GetRespondsEmployersQueryRequest : IRequest<PagedResult<GetRespondsEmployersQueryResponse>>, IPagingRequest
{
    public long EmployerId { get; set; }
    public long VacancyId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}