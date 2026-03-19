namespace Launchpad.Application.Commands.Employers.Authorize;

public class AuthorizeEmployersCommandResponse
{
    public long ProfileId { get; set; }
    public string BearerToken { get; set; } = null!;
}