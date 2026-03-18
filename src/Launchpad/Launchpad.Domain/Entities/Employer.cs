namespace Launchpad.Domain.Entities;

public class Employer
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Description { get; set; } = null!;
}