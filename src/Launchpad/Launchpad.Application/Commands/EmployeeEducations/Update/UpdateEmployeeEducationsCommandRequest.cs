using MediatR;

namespace Launchpad.Application.Commands.EmployeeEducations.Update;

public class UpdateEmployeeEducationsCommandRequest : IRequest
{
    public string Organization { get; set; } = null!;
    public string Faculty { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public int CompletionYear { get; set; }
    public int EducationLevelId { get; set; }
    public long EducationId { get; set; }
}