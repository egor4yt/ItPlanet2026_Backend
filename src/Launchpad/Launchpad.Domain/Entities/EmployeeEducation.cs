namespace Launchpad.Domain.Entities;

public class EmployeeEducation
{
    public long Id { get; set; }
    public string Organization { get; set; } = null!;
    public string Faculty { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public int CompletionYear { get; set; }
    
    public int EducationLevelId { get; set; }
    public long EmployeeId { get; set; }
    
    public virtual EducationLevel? EducationLevel { get; set; }
    public virtual Employee? Employee { get; set; }
}