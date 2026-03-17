namespace Launchpad.Domain.Entities;

public class EducationLevel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; } = null!;
}