using MediatR;

namespace Launchpad.Application.Commands.EmployerVerifications.Approve;

public class ApproveEmployerVerificationsCommandRequest : IRequest
{
    public long VerificationId { get; set; }
}