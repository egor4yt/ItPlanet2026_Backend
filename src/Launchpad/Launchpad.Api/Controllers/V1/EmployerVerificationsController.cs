using Launchpad.Api.Contracts.EmployerVerifications;
using Launchpad.Application.Commands.EmployerVerifications.Create;
using Launchpad.Application.Commands.EmployerVerifications.Update;
using Launchpad.Application.Queries.EmployerVerifications.Action;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Employer verifications controller
/// </summary>
[Route("employer-verification")]
[Authorize(JwtDetailsRole.Employer)]
public class EmployerVerificationsController : ApiControllerBase
{
    /// <summary>
    ///     Create employer verification
    /// </summary>
    /// <param name="body">Employer verification data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CreateEmployerVerificationsCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployerVerificationBody body)
    {
        var command = new CreateEmployerVerificationsCommandRequest
        {
            EmployerId = CurrentUserService.ProfileId,
            VerificationTypeId = body.VerificationTypeId,
            RequestMessage = body.RequestMessage,
            SocialNetworkLink = body.SocialNetworkLink?.Trim().ToLower(),
            TaxpayerIndividualNumber = body.TaxpayerIndividualNumber
        };

        var response = await Mediator.Send(command);

        return Created($"employer-verifications/{response.VerificationId}", response);
    }

    /// <summary>
    ///     Create employer verification
    /// </summary>
    /// <param name="body">Employer verification data</param>
    /// <param name="verificationId">Verification ID</param>
    [HttpPut("{verificationId:long}")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromRoute] long verificationId, [FromBody] UpdateEmployerVerificationBody body)
    {
        var command = new UpdateEmployerVerificationsCommandRequest
        {
            EmployerId = CurrentUserService.ProfileId,
            VerificationTypeId = body.VerificationTypeId,
            RequestMessage = body.RequestMessage,
            SocialNetworkLink = body.SocialNetworkLink?.Trim().ToLower(),
            TaxpayerIndividualNumber = body.TaxpayerIndividualNumber,
            VerificationId = verificationId
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Employer verification
    /// </summary>
    /// <param name="verificationId">Verification ID</param>
    [HttpGet("{verificationId:long}")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(GetOneEmployerVerificationsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOne([FromRoute] long verificationId)
    {
        var query = new GetOneEmployerVerificationsQueryRequest
        {
            VerificationId = verificationId
        };

        if (CurrentUserService.IsInRole(JwtDetailsRole.Employer))
            query.EmployerId = CurrentUserService.ProfileId;

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}