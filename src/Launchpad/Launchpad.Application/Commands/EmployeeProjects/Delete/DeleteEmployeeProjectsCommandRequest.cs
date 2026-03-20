using MediatR;

namespace Launchpad.Application.Commands.EmployeeProjects.Delete;

public class DeleteEmployeeProjectsCommandRequest : IRequest
{
    public long ProjectId { get; set; }
}