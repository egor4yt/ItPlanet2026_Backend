using Launchpad.Application.Queries.EducationLevels.GetAll;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EducationLevelsController
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