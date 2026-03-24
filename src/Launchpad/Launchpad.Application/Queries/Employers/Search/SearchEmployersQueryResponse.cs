namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public bool IsVerified { get; set; }
    public long? VerificationId { get; set; }
}