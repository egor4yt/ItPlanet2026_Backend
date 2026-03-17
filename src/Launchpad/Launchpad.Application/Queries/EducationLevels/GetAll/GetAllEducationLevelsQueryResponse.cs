namespace Launchpad.Application.Queries.EducationLevels.GetAll;

public class GetAllEducationLevelsQueryResponse
{
    public List<GetAllEducationLevelsQueryResponseItem> Items { get; set; } = null!;
}

public class GetAllEducationLevelsQueryResponseItem
{
    public required int Id { get; init; }
    public required string Title { get; init; }
}