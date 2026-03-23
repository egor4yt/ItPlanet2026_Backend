using MediatR;

namespace Launchpad.Application.Commands.EmployerVerifications.ChangeStatus;

public class ChangeStatusEmployerVerificationsCommandRequest : IRequest
{
    public long VerificationId { get; init; }
    public int StatusId { get; init; }
    public string? ResponseMessage { get; init; }
}