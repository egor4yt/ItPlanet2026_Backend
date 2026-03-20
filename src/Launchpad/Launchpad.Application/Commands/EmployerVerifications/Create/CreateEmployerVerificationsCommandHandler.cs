using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployerVerifications.Create;

public class CreateEmployerVerificationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployerVerificationsCommandRequest, CreateEmployerVerificationsCommandResponse>
{
    public async Task<CreateEmployerVerificationsCommandResponse> Handle(CreateEmployerVerificationsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployerVerificationsCommandResponse();

        var verificationExists = await applicationDbContext.EmployerVerifications.AnyAsync(x => x.EmployerId == request.EmployerId, cancellationToken);
        if (verificationExists) throw new BadRequestException("VerificationExists");

        var newVerification = new EmployerVerification
        {
            RequestMessage = request.RequestMessage,
            ResponseMessage = request.ResponseMessage,
            ChangedOn = DateTime.UtcNow,
            EmployerId = request.EmployerId,
            StatusId = Domain.Metadata.EmployerVerificationStatusId.Pending,
            EmployerVerificationTypeId = request.VerificationTypeId
        };
        await applicationDbContext.EmployerVerifications.AddAsync(newVerification, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.VerificationId = newVerification.Id;

        return response;
    }
}