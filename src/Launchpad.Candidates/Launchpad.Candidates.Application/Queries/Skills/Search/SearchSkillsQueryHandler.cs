using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Application.Queries.Skills.Search;

public class SearchSkillsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<SearchSkillsQueryRequest, Result<SearchSkillsQueryResponse, ErrorCollection>>
{
    public async Task<Result<SearchSkillsQueryResponse, ErrorCollection>> Handle(SearchSkillsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SearchSkillsQueryResponse();

        response.Items = await applicationDbContext.Skills
            .AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Title, $"%{request.Title}%"))
            .Take(request.Count)
            .Select(x => new SearchSkillsQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return Result.Success<SearchSkillsQueryResponse, ErrorCollection>(response);
    }
}