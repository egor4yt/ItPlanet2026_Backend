namespace Launchpad.Application.Queries.WorkFormats.GetAll;

public class GetAllWorkFormatsQueryResponse
{
    public IEnumerable<GetAllWorkFormatsQueryResponseItem> Items { get; set; } = null!;
}

public class GetAllWorkFormatsQueryResponseItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
}