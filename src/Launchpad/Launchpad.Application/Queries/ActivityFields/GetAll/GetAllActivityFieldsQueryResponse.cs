namespace Launchpad.Application.Queries.ActivityFields.GetAll;

public class GetAllActivityFieldsQueryResponse
{
    public IEnumerable<GetAllActivityFieldsQueryResponseGroup> Groups { get; set; } = null!;
}

public class GetAllActivityFieldsQueryResponseGroup
{
    public int GroupId { get; set; }
    public string Title { get; set; } = null!;
    public IEnumerable<GetAllActivityFieldsQueryResponseGroupItem> Items { get; set; } = null!;
}

public class GetAllActivityFieldsQueryResponseGroupItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}