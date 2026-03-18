using MediatR;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeesQueryRequest : IRequest<GetOneEmployeesQueryResponse>
{
    public long Id { get; set; }
}