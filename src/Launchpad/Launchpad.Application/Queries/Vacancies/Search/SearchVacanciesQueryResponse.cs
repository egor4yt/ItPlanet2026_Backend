using Launchpad.Application.SharedModels;

namespace Launchpad.Application.Queries.Vacancies.Search;

public class SearchVacanciesQueryResponse
{
    public required long Id { get; init; }
    public required string CompanyName { get; init; }
    public required string Title { get; init; }
    public required string City { get; init; }
    public bool CompanyVerified { get; set; }
    public required GeolocationPoint Coordinates { get; init; }
}