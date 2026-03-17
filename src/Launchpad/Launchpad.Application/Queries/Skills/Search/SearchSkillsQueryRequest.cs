using MediatR;

namespace Launchpad.Application.Queries.Skills.Search;

public class SearchSkillsQueryRequest : IRequest<SearchSkillsQueryResponse>
{
    public string Title { get; set; } = null!;
    public int Count { get; set; }
}