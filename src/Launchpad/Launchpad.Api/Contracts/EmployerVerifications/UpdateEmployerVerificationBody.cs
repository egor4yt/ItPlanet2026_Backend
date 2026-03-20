namespace Launchpad.Api.Contracts.EmployerVerifications;

/// <summary>
///     New employer's verification details
/// </summary>
public class UpdateEmployerVerificationBody
{
    /// <summary>
    ///     Verification type identifier
    /// </summary>
    public int VerificationTypeId { get; set; }

    /// <summary>
    ///     Request message
    /// </summary>
    public string RequestMessage { get; set; } = null!;

    /// <summary>
    ///     Social network link
    /// </summary>
    public string? SocialNetworkLink { get; set; }

    /// <summary>
    ///     Taxpayer individual number
    /// </summary>
    public string? TaxpayerIndividualNumber { get; set; } = null!;
}