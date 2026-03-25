using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneEmployeesQueryRequest, GetOneEmployeesQueryResponse>
{
    public async Task<GetOneEmployeesQueryResponse> Handle(GetOneEmployeesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await applicationDbContext.Employees
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOneEmployeesQueryResponse
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Biography = x.Biography,
                IsMale = x.IsMale,
                BirthDate = x.BirthDate,
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
                }),
                Projects = x.EmployeeProjects
                    .OrderByDescending(p => p.DateTo)
                    .Select(p => new GetOneEmployeeQueryResponseProject
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Specialization = p.Specialization,
                        Link = p.Link,
                        DateFrom = p.DateFrom,
                        DateTo = p.DateTo
                    })
            })
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException("NotFound");
    }
}