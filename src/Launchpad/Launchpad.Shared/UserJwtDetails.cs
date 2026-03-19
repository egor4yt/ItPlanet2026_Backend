namespace Launchpad.Shared;

public class JwtDetails
{
    public JwtDetails(Domain.Entities.Employee employee)
    {
        ProfileId = employee.Id.ToString();
        ContactEmail = employee.Email;
        ProfileRole = JwtDetailsRole.Employee;
    }

    public JwtDetails(Domain.Entities.Employer employer)
    {
        ProfileId = employer.Id.ToString();
        ContactEmail = employer.Email;
        ProfileRole = JwtDetailsRole.Employer;
    }

    public JwtDetails(Domain.Entities.Curator curator)
    {
        ProfileId = curator.Id.ToString();
        ContactEmail = curator.Email;
        ProfileRole = curator.IsAdmin ? JwtDetailsRole.Administrator : JwtDetailsRole.Curator;
    }

    public string ProfileId { get; }
    public string ContactEmail { get; }
    public string ProfileRole { get; }
}