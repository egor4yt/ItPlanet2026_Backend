using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeeQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneEmployeeQueryRequest, GetOneEmployeeQueryResponse>
{
    public async Task<GetOneEmployeeQueryResponse> Handle(GetOneEmployeeQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await applicationDbContext.Employees
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOneEmployeeQueryResponse
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName
            })
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException("NotFound");
    }
}