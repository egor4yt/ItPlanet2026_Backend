namespace Launchpad.Candidates.Application.Queries.Candidates.GetOne;

public class GetOneCandidatesQueryResponse
{
    public Guid InternalId { get; init; }
    public string? Biography { get; init; }
    public DateOnly? Birthdate { get; init; }
    public IEnumerable<GetOneCandidatesQueryResponseSkill> Skills { get; init; }
}

public class GetOneCandidatesQueryResponseSkill
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
}