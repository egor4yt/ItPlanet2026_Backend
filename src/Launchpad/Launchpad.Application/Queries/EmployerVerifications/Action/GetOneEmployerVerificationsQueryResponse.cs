namespace Launchpad.Application.Queries.EmployerVerifications.Action;

public class GetOneEmployerVerificationsQueryResponse
{
    public string RequestMessage { get; set; } = null!;
    public string? ResponseMessage { get; set; } = null!;
    public string? TaxpayerIndividualNumber { get; set; }
    public string? SocialNetworkLink { get; set; }
    public DateTime ChangedOn { get; set; }
    public int StatusId { get; set; }
    public int EmployerVerificationTypeId { get; set; }
}