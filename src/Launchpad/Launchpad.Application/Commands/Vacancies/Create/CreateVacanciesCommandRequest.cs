using MediatR;

namespace Launchpad.Application.Commands.Vacancies.Create;

public class CreateVacanciesCommandRequest : IRequest<CreateVacanciesCommandResponse>
{
    public required long EmployerId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }
}