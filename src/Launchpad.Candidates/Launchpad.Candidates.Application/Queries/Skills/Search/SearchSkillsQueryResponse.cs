namespace Launchpad.Candidates.Application.Queries.Skills.Search;

public class SearchSkillsQueryResponse
{
    public List<SearchSkillsQueryResponseItem> Items { get; set; } = null!;
}

public class SearchSkillsQueryResponseItem
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
}