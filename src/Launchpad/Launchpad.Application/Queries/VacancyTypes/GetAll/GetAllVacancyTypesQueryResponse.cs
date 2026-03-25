namespace Launchpad.Application.Queries.VacancyTypes.GetAll;

public class GetAllVacancyTypesQueryResponse
{
    public IEnumerable<GetAllVacancyTypesQueryResponseItem> Items { get; set; } = null!;
}

public class GetAllVacancyTypesQueryResponseItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
}