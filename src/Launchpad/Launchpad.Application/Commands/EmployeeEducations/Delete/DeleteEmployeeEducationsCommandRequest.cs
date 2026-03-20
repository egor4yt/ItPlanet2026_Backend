using MediatR;

namespace Launchpad.Application.Commands.EmployeeEducations.Delete;

public class DeleteEmployeeEducationsCommandRequest : IRequest
{
    public long EducationId { get; set; }
    public long? EmployerId { get; set; }
}