using MediatR;

namespace Launchpad.Application.Commands.Employees.Update;

public class UpdateEmployeesCommandRequest : IRequest
{
    public long EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; } = null!;
    public bool IsMale { get; set; }
    public DateOnly? BirthDate { get; set; }
}