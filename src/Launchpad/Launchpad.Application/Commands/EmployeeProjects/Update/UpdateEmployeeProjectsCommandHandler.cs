using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeProjects.Update;

public class UpdateEmployeeProjectsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateEmployeeProjectsCommandRequest>
{
    public async Task Handle(UpdateEmployeeProjectsCommandRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.EmployeeProjects
            .Where(x => x.Id == request.ProjectId);

        if (request.EmployerId.HasValue)
            query = query.Where(x => x.EmployeeId == request.EmployerId);

        await query.ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Title, request.Title)
                .SetProperty(p => p.Description, request.Description)
                .SetProperty(p => p.Specialization, request.Specialization)
                .SetProperty(p => p.Link, request.Link)
                .SetProperty(p => p.DateFrom, request.DateFrom)
                .SetProperty(p => p.DateTo, request.DateTo)
            , cancellationToken);
    }
}