namespace Launchpad.Application.Queries.VerificationStatuses.GetAll;

public class GetAllVerificationStatusesQueryResponse
{
    public IEnumerable<GetAllVerificationStatusesQueryResponseItem> Items { get; set; } = null!;
}

public class GetAllVerificationStatusesQueryResponseItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
}