namespace Launchpad.Candidates.Api.Contracts.Candidates;

/// <summary>
///     Update candidates' skills body
/// </summary>
public class UpdateCandidatesSkillsBody
{
    /// <summary>
    ///     Skills
    /// </summary>
    public required IEnumerable<UpdateCandidatesSkillsBodyItem> Skills { get; init; }
}

/// <summary>
///     Update candidates' skills body
/// </summary>
public class UpdateCandidatesSkillsBodyItem
{
    /// <summary>
    ///     Id
    /// </summary>
    public required Guid? Id { get; init; }

    /// <summary>
    ///     Title
    /// </summary>
    public required string Title { get; init; }
}