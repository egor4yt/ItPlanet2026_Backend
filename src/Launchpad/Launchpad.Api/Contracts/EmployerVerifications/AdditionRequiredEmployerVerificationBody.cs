namespace Launchpad.Api.Contracts.EmployerVerifications;

/// <summary>
///     Addition required remployer verification body
/// </summary>
public class AdditionRequiredEmployerVerificationBody
{
    /// <summary>
    ///     Reason for rejection
    /// </summary>
    public string? ResponseMessage { get; set; }
}