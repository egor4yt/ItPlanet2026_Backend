using Launchpad.Api.Contracts.Employers;
using Launchpad.Application.Commands.Employers.Update;
using Launchpad.Application.Queries.Employers.GetOne;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployersController
{
    /// <summary>
    ///     Authorized employer account details
    /// </summary>
    /// <returns>Employer data</returns>
    [Authorize(JwtDetailsRole.Employer)]
    [HttpGet]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployersQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Me()
    {
        var query = new GetOneEmployersQueryRequest
        {
            Id = CurrentUserService.ProfileId
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    ///     Update authorized employer
    /// </summary>
    [Authorize(JwtDetailsRole.Employer)]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateAuthorized([FromBody] UpdateEmployerDescriptionBody body)
    {
        var command = new UpdateEmployersCommandRequest
        {
            EmployerId = CurrentUserService.ProfileId,
            Description = body.Description,
            ActivityFieldIds = body.ActivityFieldIds ?? []
        };

        await Mediator.Send(command);

        return NoContent();
    }
}