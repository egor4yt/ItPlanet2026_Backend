using Launchpad.Application.Abstractions;
using Launchpad.Application.SharedModels;
using MediatR;

namespace Launchpad.Application.Queries.Vacancies.Search;

public class SearchVacanciesQueryRequest : IRequest<PagedResult<SearchVacanciesQueryResponse>>, IPagingRequest
{
    public string? Title { get; set; }
    public GeolocationRadiusQuery? RadiusSearch { get; set; }
    public GeolocationBoxQuery? BoxSearch { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}