using Launchpad.Candidates.Application.Queries.Skills.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class SkillsController
{
    /// <summary>
    ///     Search skills
    /// </summary>
    /// <returns>Skills</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SearchSkillsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string title = "", [FromQuery] int count = 10)
    {
        var query = new SearchSkillsQueryRequest
        {
            Title = title.ToLower().Trim(),
            Count = count
        };

        var response = await Mediator.Send(query);

        if (response.IsFailure) return StatusCode((int)response.Error.StatusCode, response.Error.Errors);
        return Ok(response.Value);
    }
}