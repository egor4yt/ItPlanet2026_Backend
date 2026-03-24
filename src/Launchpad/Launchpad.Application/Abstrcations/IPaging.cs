namespace Launchpad.Application.Abstrcations;

public interface IPaging
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}