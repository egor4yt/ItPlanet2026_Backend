namespace Launchpad.Application.Queries.EmployerVerifications.Action;

public class GetOneEmployerVerificationsQueryResponse
{
    public string CompanyName { get; set; } = null!;
    public string RequestMessage { get; set; } = null!;
    public string? ResponseMessage { get; set; } = null!;
    public string? TaxpayerIndividualNumber { get; set; }
    public string? SocialNetworkLink { get; set; }
    public DateTime ChangedOn { get; set; }
    public GetOneEmployerVerificationsQueryResponseStatus Status { get; set; } = null!;
    public GetOneEmployerVerificationsQueryResponseType Type { get; set; } = null!;
}

public class GetOneEmployerVerificationsQueryResponseType
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}

public class GetOneEmployerVerificationsQueryResponseStatus
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}