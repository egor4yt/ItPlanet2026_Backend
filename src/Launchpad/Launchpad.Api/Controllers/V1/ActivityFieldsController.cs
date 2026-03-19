using Launchpad.Application.Queries.ActivityFields.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Activity fields controller
/// </summary>
[AllowAnonymous]
[Route("activity-fields")]
public class ActivityFieldsController : ApiControllerBase
{
    /// <summary>
    ///     Get all activity fields
    /// </summary>
    /// <returns>Activity fields</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetAllActivityFieldsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var command = new GetAllActivityFieldsQueryRequest();

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}