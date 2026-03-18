namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryResponse
{
    public string CompanyName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Verified { get; set; }
}