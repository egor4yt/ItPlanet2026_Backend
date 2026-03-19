namespace Launchpad.Domain.Entities;

public class ActivityField
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ActivityFieldGroupId { get; set; }

    public virtual ActivityFieldGroup ActivityFieldGroup { get; set; }
}