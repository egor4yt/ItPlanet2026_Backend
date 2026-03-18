using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Commands.EmployeeProjects.Create;

public class CreateEmployeeProjectsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployeeProjectsCommandRequest, CreateEmployeeProjectsCommandResponse>
{
    public async Task<CreateEmployeeProjectsCommandResponse> Handle(CreateEmployeeProjectsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployeeProjectsCommandResponse();

        var newProject = new EmployeeProject
        {
            Title = request.Title,
            Description = request.Description,
            Specialization = request.Specialization,
            Link = request.Link,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            EmployeeId = request.EmployeeId
        };

        await applicationDbContext.EmployeeProjects.AddAsync(newProject, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newProject.Id;

        return response;
    }
}