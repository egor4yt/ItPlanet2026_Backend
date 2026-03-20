using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployerVerifications.Update;

public class UpdateEmployerVerificationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateEmployerVerificationsCommandRequest>
{
    public async Task Handle(UpdateEmployerVerificationsCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.EmployerVerifications
            .Where(x => x.Id == request.VerificationId && x.EmployerId == request.EmployerId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.RequestMessage, request.RequestMessage)
                    .SetProperty(p => p.EmployerVerificationTypeId, request.VerificationTypeId)
                    .SetProperty(p => p.TaxpayerIndividualNumber, request.TaxpayerIndividualNumber)
                    .SetProperty(p => p.SocialNetworkLink, request.SocialNetworkLink)
                    .SetProperty(p => p.ChangedOn, DateTime.UtcNow)
                , cancellationToken);
    }
}