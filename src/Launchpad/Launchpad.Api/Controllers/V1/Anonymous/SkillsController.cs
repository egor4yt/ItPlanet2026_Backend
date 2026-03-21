using Launchpad.Application.Queries.Skills.Search;
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
    public async Task<IActionResult> Search([FromQuery(Name = "query")] string titleQuery = "", [FromQuery] int count = 10)
    {
        var query = new SearchSkillsQueryRequest
        {
            Title = titleQuery.ToLower().Trim(),
            Count = count
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}