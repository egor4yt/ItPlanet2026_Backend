using Launchpad.Application.Queries.Skills.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Skills controller
/// </summary>
[AllowAnonymous]
[Route("skills")]
public class SkillsController : ApiControllerBase
{
    /// <summary>
    ///     Search skills
    /// </summary>
    /// <returns>Skills</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SearchSkillsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string query = "", [FromQuery] int count = 10)
    {
        var command = new SearchSkillsQueryRequest
        {
            Title = query.ToLower(),
            Count = count
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}