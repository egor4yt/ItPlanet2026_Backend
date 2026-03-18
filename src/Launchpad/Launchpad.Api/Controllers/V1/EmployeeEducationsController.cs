using Launchpad.Api.Contracts.EmployeeEducations;
using Launchpad.Application.Commands.EmployeeEducations.Create;
using Launchpad.Application.Commands.EmployeeEducations.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Education levels controller
/// </summary>
[Authorize]
[Route("employee-educations")]
public class EmployeeEducationsController : ApiControllerBase
{
    /// <summary>
    ///     Create education
    /// </summary>
    /// <returns>New project id</returns>
    [HttpPost]
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
    [ProducesResponseType(typeof(UpdateEmployeeEducationsCommandResponse), StatusCodes.Status204NoContent)]
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

        _ = await Mediator.Send(command);

        return NoContent();
    }
}