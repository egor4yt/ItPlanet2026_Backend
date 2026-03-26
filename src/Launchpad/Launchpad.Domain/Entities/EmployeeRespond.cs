namespace Launchpad.Domain.Entities;

public sealed class EmployeeRespond
{
    public long Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? CoverMessage { get; init; }
    public string? CompanyAnswer { get; init; }

    public int StatusId { get; init; }
    public long EmployeeId { get; init; }
    public long VacancyId { get; init; }

    public EmployeeRespondStatus? Status { get; init; }
    public Employee? Employee { get; init; }
    public Vacancy? Vacancy { get; init; }
}