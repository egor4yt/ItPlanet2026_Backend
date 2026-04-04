namespace Launchpad.Candidates.Api.Contracts.Candidates;

/// <summary>
///     Create candidates' body
/// </summary>
public class CreateCandidatesBody
{
    /// <summary>
    ///     First name
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    ///     Last name
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    ///     Middle name
    /// </summary>
    public string? MiddleName { get; set; }
}