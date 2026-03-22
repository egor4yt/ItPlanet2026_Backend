namespace Launchpad.Application.Queries.Skills.Search;

public class SearchSkillsQueryResponse
{
    public List<SearchSkillsQueryResponseItem> Items { get; set; } = null!;
}

public class SearchSkillsQueryResponseItem
{
    public required int Id { get; init; }
    public required string Title { get; init; }
}