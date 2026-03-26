using Launchpad.Application.Abstractions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employers.GetResponds;

public class GetRespondsEmployersQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetRespondsEmployersQueryRequest, PagedResult<GetRespondsEmployersQueryResponse>>
{
    public async Task<PagedResult<GetRespondsEmployersQueryResponse>> Handle(GetRespondsEmployersQueryRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.EmployeeResponds
            .OrderByDescending(x => x.StatusId == Domain.Metadata.EmployeeRespondStatus.Created)
            .ThenBy(x => x.CreatedAt)
            .AsNoTracking()
            .Where(x => x.VacancyId == request.VacancyId && x.Vacancy!.EmployerId == request.EmployerId);

        var count = await query.CountAsync(cancellationToken);

        var response = await query
            .Select(x => new GetRespondsEmployersQueryResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Status = new GetRespondsEmployersQueryResponseStatus
                {
                    Title = x.Status!.Title,
                    Color = x.Status.Color!.Code
                },
                Employee = new GetRespondsEmployersQueryResponseEmployee
                {
                    Id = x.EmployeeId,
                    Biography = x.Employee!.Biography,
                    FirstName = x.Employee.FirstName,
                    LastName = x.Employee.LastName,
                    MiddleName = x.Employee.MiddleName,
                    CoverMessage = x.CoverMessage
                }
            })
            .Paging(request)
            .ToListAsync(cancellationToken);

        return new PagedResult<GetRespondsEmployersQueryResponse>(response, count, request);
    }
}