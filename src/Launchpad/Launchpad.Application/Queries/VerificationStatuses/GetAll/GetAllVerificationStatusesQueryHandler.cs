using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.VerificationStatuses.GetAll;

public class GetAllVerificationStatusesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetAllVerificationStatusesQueryRequest, GetAllVerificationStatusesQueryResponse>
{
    public async Task<GetAllVerificationStatusesQueryResponse> Handle(GetAllVerificationStatusesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllVerificationStatusesQueryResponse();

        response.Items = await applicationDbContext.EmployerVerificationStatuses
            .Select(x => new GetAllVerificationStatusesQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}