using MediatR;

namespace Launchpad.Application.Commands.Employers.UpdateDescription;

public class UpdateDescriptionEmployersCommandRequest : IRequest
{
    public long EmployerId { get; set; }
    public string? Description { get; set; }
}