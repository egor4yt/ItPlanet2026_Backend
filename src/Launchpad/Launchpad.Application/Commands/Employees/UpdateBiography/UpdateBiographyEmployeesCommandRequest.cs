using MediatR;

namespace Launchpad.Application.Commands.Employees.UpdateBiography;

public class UpdateBiographyEmployeesCommandRequest : IRequest
{
    public long EmployeeId { get; set; }
    public string Biography { get; set; } = null!;
}