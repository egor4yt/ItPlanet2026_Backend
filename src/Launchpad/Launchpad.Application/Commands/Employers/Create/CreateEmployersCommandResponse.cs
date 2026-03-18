namespace Launchpad.Application.Commands.Employers.Create;

public class CreateEmployersCommandResponse
{
    public long EmployerId { get; set; }
    public string BearerToken { get; set; } = null!;
}