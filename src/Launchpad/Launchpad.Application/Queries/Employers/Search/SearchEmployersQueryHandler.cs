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

        if (request.VerificationStatusId.Count > 0) query = query.Where(x => request.VerificationStatusId.Contains(x.Verification!.StatusId));
        if (!string.IsNullOrWhiteSpace(request.Name)) query = query.Where(x => EF.Functions.ILike(x.CompanyName, $"%{request.Name}%"));

        var totalCount = await query.CountAsync(cancellationToken);

        var employers = await query
            .Select(x => new SearchEmployersQueryResponse
            {
                Id = x.Id,
                Name = x.CompanyName,
                IsVerified = x.Verification != null && x.Verification.StatusId == Domain.Metadata.EmployerVerificationStatusId.Approved,
                VerificationId = x.Verification != null ? x.Verification.Id : null
            })
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<SearchEmployersQueryResponse>(employers, totalCount, request);
    }
}