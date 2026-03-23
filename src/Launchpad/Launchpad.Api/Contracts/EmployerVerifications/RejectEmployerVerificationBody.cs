namespace Launchpad.Api.Contracts.EmployerVerifications;

/// <summary>
///     Reject remployer verification body
/// </summary>
public class RejectEmployerVerificationBody
{
    /// <summary>
    ///     Reason for rejection
    /// </summary>
    public string? ResponseMessage { get; set; }
}