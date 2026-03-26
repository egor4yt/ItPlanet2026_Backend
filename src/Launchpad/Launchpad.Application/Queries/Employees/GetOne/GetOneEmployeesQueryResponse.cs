namespace Launchpad.Application.Queries.Employees.GetOne;

public class GetOneEmployeesQueryResponse
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? Biography { get; set; }
    public bool? IsMale { get; set; }
    public DateOnly? BirthDate { get; set; }
    public IEnumerable<GetOneEmployeeQueryResponseSkill> Skills { get; set; } = null!;
    public IEnumerable<GetOneEmployeeQueryResponseEducation> Education { get; set; } = null!;
    public IEnumerable<GetOneEmployeeQueryResponseProject> Projects { get; set; } = null!;
}

public class GetOneEmployeeQueryResponseSkill
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}

public class GetOneEmployeeQueryResponseEducation
{
    public long Id { get; set; }
    public string Organization { get; set; } = null!;
    public string Faculty { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public int CompletionYear { get; set; }
    public GetOneEmployeeQueryResponseEducationLevel EducationLevel { get; set; } = null!;
}

public class GetOneEmployeeQueryResponseEducationLevel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}

public class GetOneEmployeeQueryResponseProject
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string? Link { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
}