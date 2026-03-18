namespace Launchpad.Shared;

public class JwtDetails
{
    public JwtDetails(Domain.Entities.Employee employee)
    {
        ProfileId = employee.Id.ToString();
        ContactEmail = employee.Email;
        ProfileRole = JwtDetailsRole.Employee;
    }

    public JwtDetails(Domain.Entities.Employer employee)
    {
        ProfileId = employee.Id.ToString();
        ContactEmail = employee.Email;
        ProfileRole = JwtDetailsRole.Employer;
    }

    public string ProfileId { get; }
    public string ContactEmail { get; }
    public string ProfileRole { get; }
}