namespace Launchpad.Application.Queries.Employers.GetResponds;

public class GetRespondsEmployersQueryResponse
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required GetRespondsEmployersQueryResponseStatus Status { get; set; }
    public required GetRespondsEmployersQueryResponseEmployee Employee { get; set; }
}

public class GetRespondsEmployersQueryResponseEmployee
{
    public long Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Biography { get; set; }
    public string? CoverMessage { get; set; }
}

public class GetRespondsEmployersQueryResponseStatus
{
    public required string Title { get; set; }
    public required string Color { get; set; }
}