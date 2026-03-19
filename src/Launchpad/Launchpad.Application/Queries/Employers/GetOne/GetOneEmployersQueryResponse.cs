namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryResponse
{
    public string CompanyName { get; set; } = null!;
    public bool Verified { get; set; }
    public string? Description { get; set; }
    public string? ActivityFields { get; set; }
}