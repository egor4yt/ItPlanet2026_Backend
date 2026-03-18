using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneEmployersQueryRequest, GetOneEmployersQueryResponse>
{
    public async Task<GetOneEmployersQueryResponse> Handle(GetOneEmployersQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await applicationDbContext.Employers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOneEmployersQueryResponse
            {
                CompanyName = x.CompanyName,
                Description = x.Description,
                Verified = false
            })
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException("NotFound");
    }
}