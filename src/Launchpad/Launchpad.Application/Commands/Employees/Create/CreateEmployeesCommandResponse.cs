namespace Launchpad.Application.Commands.Employees.Create;

public class CreateEmployeesCommandResponse
{
    public long EmployeeId { get; set; }
    public string BearerToken { get; set; } = null!;
}