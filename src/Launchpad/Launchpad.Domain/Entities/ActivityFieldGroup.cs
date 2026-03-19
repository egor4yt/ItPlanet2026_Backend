namespace Launchpad.Domain.Entities;

public class ActivityFieldGroup
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public virtual ICollection<ActivityField> ActivityFields { get; set; }
}