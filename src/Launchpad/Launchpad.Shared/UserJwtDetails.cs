namespace Launchpad.Shared;

public class UserJwtDetails(Domain.Entities.User user)
{
    public long Id { get; set; } = user.Id;
    public string Email { get; set; } = user.Email;
}