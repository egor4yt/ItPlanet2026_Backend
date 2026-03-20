using Launchpad.Api.Contracts.EmployerVerifications;
using Launchpad.Application.Commands.EmployerVerifications.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Employer verifications controller
/// </summary>
[Route("employer-verification")]
public class EmployerVerificationsController : ApiControllerBase
{
    /// <summary>
    ///     Create employer verification
    /// </summary>
    /// <param name="body">Verification employer data</param>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CreateEmployerVerificationsCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployerVerificationBody body)
    {
        var command = new CreateEmployerVerificationsCommandRequest
        {
            EmployerId = CurrentUserService.ProfileId,
            VerificationTypeId = body.VerificationTypeId,
            RequestMessage = body.RequestMessage,
            ResponseMessage = body.ResponseMessage
        };

        var response = await Mediator.Send(command);

        return Created($"employer-verifications/{response.VerificationId}", response);
    }
}