namespace Launchpad.Candidates.Application.Abstractions;

public interface IPagingRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}