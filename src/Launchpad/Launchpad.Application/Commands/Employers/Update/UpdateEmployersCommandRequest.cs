using MediatR;

namespace Launchpad.Application.Commands.Employers.Update;

public class UpdateEmployersCommandRequest : IRequest
{
    public long EmployerId { get; set; }
    public string? Description { get; set; }
    public required List<int> ActivityFieldIds { get; set; }
}