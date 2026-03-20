using MediatR;

namespace Launchpad.Application.Commands.EmployerVerifications.Create;

public class CreateEmployerVerificationsCommandRequest : IRequest<CreateEmployerVerificationsCommandResponse>
{
    public long EmployerId { get; set; }
    public int VerificationTypeId { get; set; }
    public string RequestMessage { get; set; } = null!;
    public string TaxpayerIndividualNumber { get; set; } = null!;
    public string SocialNetworkLink { get; set; } = null!;
}