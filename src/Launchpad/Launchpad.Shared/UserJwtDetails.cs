namespace Launchpad.Shared;

public class JwtDetails
{
    public JwtDetails(Domain.Entities.Employee employee)
    {
        Id = employee.Id;
        Email = employee.Email;
        IsEmployee = true;
    }

    public JwtDetails()
    {
        Id = 0;
        Email = string.Empty;
        IsEmployee = false;
    }

    public long Id { get; }
    public string Email { get; }
    public bool IsEmployee { get; }
}