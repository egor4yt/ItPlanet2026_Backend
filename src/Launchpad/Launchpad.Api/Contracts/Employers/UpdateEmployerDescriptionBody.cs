namespace Launchpad.Api.Contracts.Employers;

/// <summary>
///     New employer description
/// </summary>
public class UpdateEmployerDescriptionBody
{
    /// <summary>
    ///     New employer description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    ///     New employer activity fields
    /// </summary>
    public List<int>? ActivityFieldIds { get; init; }
}