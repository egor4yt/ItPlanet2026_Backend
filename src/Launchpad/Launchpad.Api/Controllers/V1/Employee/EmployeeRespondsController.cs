using Launchpad.Api.Contracts.EmployeeResponds;
using Launchpad.Application.Commands.EmployeeResponds.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeeRespondsController
{
    /// <summary>
    ///     Create respond to vacancy
    /// </summary>
    [HttpPost]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateEmployeeRespondsCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRespondBody request)
    {
        if (CurrentUserService.ProfileId != request.EmployeeId && CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            throw new ForbiddenException("UseYourProfileId");

        var command = new CreateEmployeeRespondsCommandRequest
        {
            EmployeeId = request.EmployeeId,
            VacancyId = request.VacancyId,
            CoverMessage = request.CoverMessage
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}