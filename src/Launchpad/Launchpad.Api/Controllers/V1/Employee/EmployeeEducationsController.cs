using Launchpad.Api.Contracts.EmployeeEducations;
using Launchpad.Application.Commands.EmployeeEducations.Create;
using Launchpad.Application.Commands.EmployeeEducations.Delete;
using Launchpad.Application.Commands.EmployeeEducations.Update;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeeEducationsController
{
    /// <summary>
    ///     Create education
    /// </summary>
    /// <returns>New project id</returns>
    [HttpPost]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(typeof(CreateEmployeeEducationsCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeEducationBody body)
    {
        var command = new CreateEmployeeEducationsCommandRequest
        {
            Organization = body.Organization,
            Faculty = body.Faculty,
            Specialization = body.Specialization,
            CompletionYear = body.CompletionYear,
            EducationLevelId = body.EducationLevelId,
            EmployeeId = CurrentUserService.ProfileId
        };

        var response = await Mediator.Send(command);

        return Created("", response);
    }

    /// <summary>
    ///     Update education
    /// </summary>
    [HttpPut("{educationId:long}")]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] long educationId, [FromBody] UpdateEmployeeEducationBody body)
    {
        var command = new UpdateEmployeeEducationsCommandRequest
        {
            Organization = body.Organization,
            Faculty = body.Faculty,
            Specialization = body.Specialization,
            CompletionYear = body.CompletionYear,
            EducationLevelId = body.EducationLevelId,
            EducationId = educationId
        };

        if (CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            command.EmployerId = CurrentUserService.ProfileId;

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Delete education
    /// </summary>
    [HttpDelete("{educationId:long}")]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete([FromRoute] long educationId)
    {
        var command = new DeleteEmployeeEducationsCommandRequest
        {
            EducationId = educationId
        };

        if (CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            command.EmployerId = CurrentUserService.ProfileId;

        await Mediator.Send(command);

        return NoContent();
    }
}