namespace Launchpad.Application.Commands.Employees.Create;

public class CreateEmployeeCommandResponse
{
    public long EmployeeId { get; set; }
    public string BearerToken { get; set; } = null!;
}