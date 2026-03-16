namespace Launchpad.Application.Commands.Users.Create;

public class CreateEmployeeCommandResponse
{
    public long EmployeeId { get; set; }
    public string BearerToken { get; set; } = null!;
}