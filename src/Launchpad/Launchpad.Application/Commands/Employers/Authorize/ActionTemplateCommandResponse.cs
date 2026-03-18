namespace Launchpad.Application.Commands.Employers.Authorize;

public class AuthorizeEmployersCommandResponse
{
    public long EmployerId { get; set; }
    public string BearerToken { get; set; } = null!;
}