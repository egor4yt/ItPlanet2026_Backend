using MediatR;

namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryRequest : IRequest<GetOneEmployersQueryResponse>
{
    public long Id { get; set; }
}