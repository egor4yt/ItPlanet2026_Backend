using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeEducations.Delete;

public class DeleteEmployeeEducationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<DeleteEmployeeEducationsCommandRequest>
{
    public async Task Handle(DeleteEmployeeEducationsCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.EmployeeEducations
            .Where(x => x.Id == request.EducationId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}