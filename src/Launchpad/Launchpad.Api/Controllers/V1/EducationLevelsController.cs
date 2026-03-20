using Launchpad.Application.Queries.EducationLevels.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Education levels controller
/// </summary>
[AllowAnonymous]
[Route("education-levels")]
public class EducationLevelsController : ApiControllerBase
{
    /// <summary>
    ///     Get all education levels
    /// </summary>
    /// <returns>Education levels</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetAllEducationLevelsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var query = new GetAllEducationLevelsQueryRequest();

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}