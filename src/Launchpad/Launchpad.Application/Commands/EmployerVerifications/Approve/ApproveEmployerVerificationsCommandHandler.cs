using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployerVerifications.Approve;

public class ApproveEmployerVerificationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<ApproveEmployerVerificationsCommandRequest>
{
    public async Task Handle(ApproveEmployerVerificationsCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.EmployerVerifications
            .Where(x => x.Id == request.VerificationId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.StatusId, Domain.Metadata.EmployerVerificationStatusId.Approved)
                , cancellationToken);
    }
}