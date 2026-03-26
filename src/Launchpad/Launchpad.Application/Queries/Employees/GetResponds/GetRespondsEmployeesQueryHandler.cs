using Launchpad.Application.Abstractions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employees.GetResponds;

public class GetRespondsEmployeesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetRespondsEmployeesQueryRequest, PagedResult<GetRespondsEmployeesQueryResponse>>
{
    public async Task<PagedResult<GetRespondsEmployeesQueryResponse>> Handle(GetRespondsEmployeesQueryRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.EmployeeResponds
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .Where(x => x.EmployeeId == request.EmployeeId);

        var count = await query.CountAsync(cancellationToken);

        var response = await query
            .Select(x => new GetRespondsEmployeesQueryResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Company = new GetRespondsEmployeesQueryResponseCompany
                {
                    Id = x.Vacancy!.EmployerId,
                    Name = x.Vacancy.Employer!.CompanyName
                },
                Vacancy = new GetRespondsEmployeesQueryResponseVacancy
                {
                    Id = x.VacancyId,
                    Title = x.Vacancy.Title
                },
                Status = new GetRespondsEmployeesQueryResponseStatus
                {
                    Title = x.Status!.Description,
                    Color = x.Status.Color!.Code
                }
            })
            .Paging(request)
            .ToListAsync(cancellationToken);

        return new PagedResult<GetRespondsEmployeesQueryResponse>(response, count, request);
    }
}