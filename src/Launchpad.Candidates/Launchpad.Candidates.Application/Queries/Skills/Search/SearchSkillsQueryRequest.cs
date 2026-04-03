using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Queries.Skills.Search;

public class SearchSkillsQueryRequest : IRequest<Result<SearchSkillsQueryResponse, ErrorCollection>>
{
    public string Title { get; set; } = null!;
    public int Count { get; set; }
}