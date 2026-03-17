using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Skills.Search;

public class SearchSkillsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<SearchSkillsQueryRequest, SearchSkillsQueryResponse>
{
    public async Task<SearchSkillsQueryResponse> Handle(SearchSkillsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SearchSkillsQueryResponse();

        response.Items = await applicationDbContext.Skills
            .Where(x => EF.Functions.ILike(x.Title, $"%{request.Title}%"))
            .Take(request.Count)
            .Select(x => new SearchSkillsQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}