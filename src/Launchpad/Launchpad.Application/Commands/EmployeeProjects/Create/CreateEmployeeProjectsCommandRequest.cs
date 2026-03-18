using MediatR;

namespace Launchpad.Application.Commands.EmployeeProjects.Create;

public class CreateEmployeeProjectsCommandRequest : IRequest<CreateEmployeeProjectsCommandResponse>
{
    public long EmployeeId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string? Link { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
}