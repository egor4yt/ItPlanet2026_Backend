namespace Launchpad.Candidates.Api.Contracts.Candidates;

/// <summary>
///     Update candidates' body
/// </summary>
public class UpdateCandidatesBody
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

    /// <summary>
    ///     Biography
    /// </summary>
    public string? Biography { get; set; }

    /// <summary>
    ///     Birthdate
    /// </summary>
    public DateOnly? Birthdate { get; set; }
}