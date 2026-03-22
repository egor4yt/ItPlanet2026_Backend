using Launchpad.Application.Commands.EmployerVerifications.Approve;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployerVerificationsController
{
    /// <summary>
    ///     Approve employer verification
    /// </summary>
    /// <param name="verificationId">Verification id</param>
    [HttpPost("{verificationId:long}/approve")]
    [Authorize(JwtDetailsRole.Curator)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Approve([FromRoute] long verificationId)
    {
        var command = new ApproveEmployerVerificationsCommandRequest
        {
            VerificationId = verificationId
        };

        await Mediator.Send(command);

        return NoContent();
    }
}