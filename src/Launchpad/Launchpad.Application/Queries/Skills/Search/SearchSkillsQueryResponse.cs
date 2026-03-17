namespace Launchpad.Application.Queries.Skills.Search;

public class SearchSkillsQueryResponse
{
    public List<SearchSkillsQueryResponseItem> Items { get; set; }
}

public class SearchSkillsQueryResponseItem
{
    public required int Id { get; init; }
    public required string Title { get; init; }
}