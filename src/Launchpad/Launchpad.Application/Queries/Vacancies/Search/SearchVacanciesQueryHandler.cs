using Launchpad.Application.Abstractions;
using Launchpad.Application.SharedModels;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Vacancies.Search;

public class SearchVacanciesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<SearchVacanciesQueryRequest, PagedResult<SearchVacanciesQueryResponse>>
{
    public async Task<PagedResult<SearchVacanciesQueryResponse>> Handle(SearchVacanciesQueryRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.Vacancies.AsQueryable().AsNoTracking();

        var count = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query
                .Where(x => EF.Functions.ILike(x.Title, $"%{request.Title}%"))
                .OrderBy(x => x.CreatedAt);

        if (request.RadiusSearch != null)
        {
            var fromPoint = GeographyHelper.CreatePoint(request.RadiusSearch.Point.Longitude, request.RadiusSearch.Point.Latitude);
            query = query
                .Where(x => x.Location.IsWithinDistance(fromPoint, request.RadiusSearch.RadiusInMeters))
                .OrderBy(x => x.Location.Distance(fromPoint));
        }

        if (request.BoxSearch != null)
        {
            var geometry = request.BoxSearch.ToNetTopologyGeometry();

            query = query
                .Where(x => x.Location.Intersects(geometry))
                .OrderBy(x => x.CreatedAt);
        }

        if (string.IsNullOrWhiteSpace(request.Title) && request.RadiusSearch == null && request.BoxSearch == null)
            query = query.OrderBy(x => x.CreatedAt);

        var results = await query
            .Select(x => new SearchVacanciesQueryResponse
            {
                Id = x.Id,
                CompanyName = x.Employer!.CompanyName,
                Title = x.Title,
                City = x.City,
                CompanyVerified = x.Employer.Verification!.StatusId == Domain.Metadata.EmployerVerificationStatusId.Approved,
                Coordinates = new GeolocationPoint
                {
                    Longitude = x.Location.Coordinate.X,
                    Latitude = x.Location.Coordinate.Y
                }
            })
            .Paging(request)
            .ToListAsync(cancellationToken);

        return new PagedResult<SearchVacanciesQueryResponse>(results, count, request);
    }
}