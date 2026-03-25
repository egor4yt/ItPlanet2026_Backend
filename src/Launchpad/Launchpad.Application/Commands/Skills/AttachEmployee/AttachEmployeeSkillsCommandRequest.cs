using MediatR;

namespace Launchpad.Application.Commands.Skills.AttachEmployee;

public class AttachEmployeeSkillsCommandRequest : IRequest
{
    public long EmployeeId { get; set; }
    public IEnumerable<AttachEmployeeSkillsCommandRequestItem> Skills { get; set; } = null!;
}

public class AttachEmployeeSkillsCommandRequestItem
{
    public int? Id { get; set; }
    public string Title { get; set; } = null!;
}