namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeeQueryResponse
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public IEnumerable<GetOneEmployeeQueryResponseSkill> Skills { get; set; } = null!;
}

public class GetOneEmployeeQueryResponseSkill
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}