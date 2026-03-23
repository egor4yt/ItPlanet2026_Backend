using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployerVerifications.ChangeStatus;

public class ChangeStatusEmployerVerificationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<ChangeStatusEmployerVerificationsCommandRequest>
{
    public async Task Handle(ChangeStatusEmployerVerificationsCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.EmployerVerifications
            .Where(x => x.Id == request.VerificationId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.StatusId, request.StatusId)
                    .SetProperty(p => p.ResponseMessage, request.ResponseMessage)
                , cancellationToken);
    }
}