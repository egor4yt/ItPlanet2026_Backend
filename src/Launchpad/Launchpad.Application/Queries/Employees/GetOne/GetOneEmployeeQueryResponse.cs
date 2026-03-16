namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeeQueryResponse
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
}