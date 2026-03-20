using MediatR;

namespace Launchpad.Application.Commands.EmployerVerifications.Update;

public class UpdateEmployerVerificationsCommandRequest : IRequest
{
    public long EmployerId { get; set; }
    public long VerificationId { get; set; }
    public int VerificationTypeId { get; set; }
    public string RequestMessage { get; set; } = null!;
    public string? TaxpayerIndividualNumber { get; set; }
    public string? SocialNetworkLink { get; set; }
}