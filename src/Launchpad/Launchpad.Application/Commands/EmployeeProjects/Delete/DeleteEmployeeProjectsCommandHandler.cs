using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeProjects.Delete;

public class DeleteEmployeeProjectsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<DeleteEmployeeProjectsCommandRequest>
{
    public async Task Handle(DeleteEmployeeProjectsCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.EmployeeProjects
            .Where(x => x.Id == request.ProjectId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}