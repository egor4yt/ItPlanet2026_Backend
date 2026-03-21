using Launchpad.Api.Contracts.EmployeeProjects;
using Launchpad.Application.Commands.EmployeeProjects.Create;
using Launchpad.Application.Commands.EmployeeProjects.Delete;
using Launchpad.Application.Commands.EmployeeProjects.Update;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeeProjectsController
{
    /// <summary>
    ///     Create a project
    /// </summary>
    /// <returns>Education levels</returns>
    [HttpPost]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(typeof(CreateEmployeeProjectsCommandResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeProjectBody body)
    {
        var command = new CreateEmployeeProjectsCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            Title = body.Title,
            Description = body.Description,
            Specialization = body.Specialization,
            Link = body.Link,
            DateFrom = body.DateFrom,
            DateTo = body.DateTo
        };

        var response = await Mediator.Send(command);

        return Created("", response);
    }

    /// <summary>
    ///     Update project
    /// </summary>
    [HttpPut("{projectId:long}")]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] long projectId, [FromBody] UpdateEmployeeProjectBody body)
    {
        var command = new UpdateEmployeeProjectsCommandRequest
        {
            ProjectId = projectId,
            Title = body.Title,
            Description = body.Description,
            Specialization = body.Specialization,
            Link = body.Link,
            DateFrom = body.DateFrom,
            DateTo = body.DateTo
        };

        if (CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            command.EmployerId = CurrentUserService.ProfileId;

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Delete a project
    /// </summary>
    [HttpDelete("{projectId:long}")]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete([FromRoute] long projectId)
    {
        var command = new DeleteEmployeeProjectsCommandRequest
        {
            ProjectId = projectId
        };

        if (CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            command.EmployerId = CurrentUserService.ProfileId;

        await Mediator.Send(command);

        return NoContent();
    }
}