using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employers.Update;

public class UpdateEmployersCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateEmployersCommandRequest>
{
    public async Task Handle(UpdateEmployersCommandRequest request, CancellationToken cancellationToken)
    {
        var employer = await applicationDbContext.Employers
            .Include(x => x.ActivityFields)
            .FirstOrDefaultAsync(x => x.Id == request.EmployerId, cancellationToken);
        if (employer == null) throw new NotFoundException("EmployerNotExist.");

        var activityFields = await applicationDbContext.ActivityFields
            .Where(x => request.ActivityFieldIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        employer.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description;
        employer.ActivityFields = activityFields;

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}