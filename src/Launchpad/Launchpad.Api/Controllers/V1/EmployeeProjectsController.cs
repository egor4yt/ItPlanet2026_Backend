using Launchpad.Api.Contracts.EmployeeProjects;
using Launchpad.Application.Commands.EmployeeProjects.Create;
using Launchpad.Application.Commands.EmployeeProjects.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Education levels controller
/// </summary>
[Authorize]
[Route("employee-projects")]
public class EmployeeProjectsController : ApiControllerBase
{
    /// <summary>
    ///     Create a project
    /// </summary>
    /// <returns>Education levels</returns>
    [HttpPost]
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
    [ProducesResponseType(typeof(UpdateEmployeeProjectsCommandResponse), StatusCodes.Status204NoContent)]
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

        _ = await Mediator.Send(command);

        return NoContent();
    }
}