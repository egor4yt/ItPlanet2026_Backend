namespace Launchpad.Application.Commands.Employees.Authorize;

public class AuthorizeEmployeeCommandResponse
{
    public long EmployeeId { get; set; }
    public string BearerToken { get; set; } = null!;
}