using MediatR;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeeQueryRequest : IRequest<GetOneEmployeeQueryResponse>
{
    public long Id { get; set; }
}