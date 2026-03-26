namespace Launchpad.Application.Queries.Employees.GetResponds;

public class GetRespondsEmployeesQueryResponse
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required GetRespondsEmployeesQueryResponseVacancy Vacancy { get; set; }
    public required GetRespondsEmployeesQueryResponseCompany Company { get; set; }
    public required GetRespondsEmployeesQueryResponseStatus Status { get; set; }
}

public class GetRespondsEmployeesQueryResponseCompany
{
    public long Id { get; set; }
    public required string Name { get; set; }
}

public class GetRespondsEmployeesQueryResponseVacancy
{
    public long Id { get; set; }
    public required string Title { get; set; }
}

public class GetRespondsEmployeesQueryResponseStatus
{
    public required string Title { get; set; }
    public required string Color { get; set; }
}