using Launchpad.Application.SharedModels;
using MediatR;

namespace Launchpad.Application.Commands.Vacancies.Create;

public class CreateVacanciesCommandRequest : IRequest<CreateVacanciesCommandResponse>
{
    public required long EmployerId { get; init; }
    public required int TypeId { get; init; }
    public required string Title { get; init; }
    public required DateTime? StartDate { get; init; }
    public required DateTime? EndDate { get; init; }
    public required string Description { get; init; }
    public required string City { get; init; }
    public required string FullAddress { get; init; }
    public required GeolocationPoint Location { get; init; }
    public required List<int> WorkFormatIds { get; init; }
    public IEnumerable<CreateVacanciesCommandRequestSkill> Skills { get; set; } = null!;
}

public class CreateVacanciesCommandRequestSkill
{
    public int? Id { get; set; }
    public string Title { get; set; } = null!;
}