using MediatR;

namespace Launchpad.Application.Commands.EmployeeEducations.Create;

public class CreateEmployeeEducationsCommandRequest : IRequest<CreateEmployeeEducationsCommandResponse>
{
    public string Organization { get; set; } = null!;
    public string Faculty { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public int CompletionYear { get; set; }
    public int EducationLevelId { get; set; }
    public long EmployeeId { get; set; }
}