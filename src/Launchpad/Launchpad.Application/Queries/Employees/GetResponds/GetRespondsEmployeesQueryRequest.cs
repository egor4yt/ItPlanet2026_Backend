using Launchpad.Application.Abstractions;
using MediatR;

namespace Launchpad.Application.Queries.Employees.GetResponds;

public class GetRespondsEmployeesQueryRequest : IRequest<PagedResult<GetRespondsEmployeesQueryResponse>>, IPagingRequest
{
    public long EmployeeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}