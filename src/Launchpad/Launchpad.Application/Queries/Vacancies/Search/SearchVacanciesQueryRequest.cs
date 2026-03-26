using Launchpad.Application.Abstractions;
using Launchpad.Application.SharedModels;
using MediatR;

namespace Launchpad.Application.Queries.Vacancies.Search;

public class SearchVacanciesQueryRequest : IRequest<PagedResult<SearchVacanciesQueryResponse>>, IPagingRequest
{
    public required string? Title { get; init; }
    public required List<long>? IncludeIds { get; init; }
    public required GeolocationRadiusQuery? RadiusSearch { get; init; }
    public required GeolocationBoxQuery? BoxSearch { get; init; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}