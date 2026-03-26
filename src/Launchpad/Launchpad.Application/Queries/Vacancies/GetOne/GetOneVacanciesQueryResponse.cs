using Launchpad.Application.SharedModels;

namespace Launchpad.Application.Queries.Vacancies.GetOne;

public class GetOneVacanciesQueryResponse
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string WorkFormat { get; init; }
    public required string Type { get; init; }
    public required DateTime? StartDate { get; init; }
    public required DateTime? EndDate { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required GetOneVacanciesQueryResponseEmployer Employer { get; set; }
    public required GetOneVacanciesQueryResponseLocation Location { get; set; }
}

public class GetOneVacanciesQueryResponseEmployer
{
    public long Id { get; init; }
    public required string CompanyName { get; init; }
    public bool IsVerified { get; set; }
}

public class GetOneVacanciesQueryResponseLocation
{
    public required string City { get; init; }
    public required string FullAddress { get; init; }
    public required GeolocationPoint Coordinates { get; set; }
}