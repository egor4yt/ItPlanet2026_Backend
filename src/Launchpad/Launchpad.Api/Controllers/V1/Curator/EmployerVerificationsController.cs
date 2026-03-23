using Launchpad.Api.Contracts.EmployerVerifications;
using Launchpad.Application.Commands.EmployerVerifications.ChangeStatus;
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Approve([FromRoute] long verificationId)
    {
        var command = new ChangeStatusEmployerVerificationsCommandRequest
        {
            VerificationId = verificationId,
            StatusId = Domain.Metadata.EmployerVerificationStatusId.Approved,
            ResponseMessage = null
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Reject employer verification
    /// </summary>
    /// <param name="verificationId">Verification id</param>
    /// <param name="body">Body</param>
    [HttpPost("{verificationId:long}/reject")]
    [Authorize(JwtDetailsRole.Curator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Reject([FromRoute] long verificationId, [FromBody] RejectEmployerVerificationBody body)
    {
        var command = new ChangeStatusEmployerVerificationsCommandRequest
        {
            VerificationId = verificationId,
            StatusId = Domain.Metadata.EmployerVerificationStatusId.Rejected,
            ResponseMessage = body.ResponseMessage
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Reject employer verification
    /// </summary>
    /// <param name="verificationId">Verification id</param>
    /// <param name="body">Body</param>
    [HttpPost("{verificationId:long}/addition-required")]
    [Authorize(JwtDetailsRole.Curator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdditionRequired([FromRoute] long verificationId, [FromBody] AdditionRequiredEmployerVerificationBody body)
    {
        var command = new ChangeStatusEmployerVerificationsCommandRequest
        {
            VerificationId = verificationId,
            StatusId = Domain.Metadata.EmployerVerificationStatusId.WaitingEmployer,
            ResponseMessage = body.ResponseMessage
        };

        await Mediator.Send(command);

        return NoContent();
    }
}