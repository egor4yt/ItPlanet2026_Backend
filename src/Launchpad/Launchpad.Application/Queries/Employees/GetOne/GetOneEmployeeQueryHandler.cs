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
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOneEmployeeQueryResponse
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Skills = x.Skills.Select(s => new GetOneEmployeeQueryResponseSkill
                {
                    Id = s.Id,
                    Title = s.Title
                }),
                Education = x.EmployeeEducations.Select(e => new GetOneEmployeeQueryResponseEducation
                {
                    Id = e.Id,
                    Organization = e.Organization,
                    Faculty = e.Faculty,
                    Specialization = e.Specialization,
                    CompletionYear = e.CompletionYear,
                    EducationLevel = new GetOneEmployeeQueryResponseEducationLevel
                    {
                        Id = e.EducationLevel!.Id,
                        Title = e.EducationLevel.Title
                    }
                })
            })
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException("NotFound");
    }
}