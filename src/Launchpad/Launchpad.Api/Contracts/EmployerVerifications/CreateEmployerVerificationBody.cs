namespace Launchpad.Api.Contracts.EmployerVerifications;

/// <summary>
///     Create an employer's verification
/// </summary>
public class CreateEmployerVerificationBody
{
    /// <summary>
    ///     Verification type identifier
    /// </summary>
    public int VerificationTypeId { get; set; }

    /// <summary>
    ///     Request message
    /// </summary>
    public string RequestMessage { get; set; } = null!;
}