using Launchpad.Application.Abstrcations;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employers.Search;

public class SearchEmployersQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<SearchEmployersQueryRequest, PagedResult<SearchEmployersQueryResponse>>
{
    public async Task<PagedResult<SearchEmployersQueryResponse>> Handle(SearchEmployersQueryRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.Employers.AsNoTracking();

        if (request.VerificationStatusId.HasValue) query = query.Where(x => x.Verification!.StatusId == request.VerificationStatusId);
        if (!string.IsNullOrWhiteSpace(request.Name)) query = query.Where(x => EF.Functions.ILike(x.CompanyName, $"%{request.Name}%"));

        var totalCount = await query.CountAsync(cancellationToken);

        var employers = await query
            .Select(x => new SearchEmployersQueryResponse
            {
                Id = x.Id,
                Name = x.CompanyName
            })
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<SearchEmployersQueryResponse>(employers, totalCount, request);
    }
}