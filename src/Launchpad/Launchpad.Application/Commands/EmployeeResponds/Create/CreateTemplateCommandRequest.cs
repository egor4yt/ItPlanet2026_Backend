using MediatR;

namespace Launchpad.Application.Commands.EmployeeResponds.Create;

public class CreateEmployeeRespondsCommandRequest : IRequest<CreateEmployeeRespondsCommandResponse>
{
    public long EmployeeId { get; set; }
    public long VacancyId { get; set; }
    public string? CoverMessage { get; set; }
}