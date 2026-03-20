using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeProjects.Delete;

public class DeleteEmployeeProjectsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<DeleteEmployeeProjectsCommandRequest>
{
    public async Task Handle(DeleteEmployeeProjectsCommandRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.EmployeeProjects
            .Where(x => x.Id == request.ProjectId);

        if (request.EmployerId.HasValue)
            query = query.Where(x => x.EmployeeId == request.EmployerId);

        await query.ExecuteDeleteAsync(cancellationToken);
    }
}