using Launchpad.Api.Contracts.EmployeeEducations;
using Launchpad.Application.Commands.EmployeeEducations.Create;
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
    /// <returns>Education levels</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateEmployeeEducationsCommandResponse), StatusCodes.Status201Created)]
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

        return Ok(response);
    }
}