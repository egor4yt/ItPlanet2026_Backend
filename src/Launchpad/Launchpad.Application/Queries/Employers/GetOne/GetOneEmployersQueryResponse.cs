namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryResponse
{
    public string CompanyName { get; set; } = null!;
    public string? Description { get; set; }
    public string? ActivityFields { get; set; }
    public GetOneEmployersQueryResponseVerification? Verification { get; set; }
}

public class GetOneEmployersQueryResponseVerification
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
}