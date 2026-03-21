using Launchpad.Application.Queries.ActivityFields.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class ActivityFieldsController
{
    /// <summary>
    ///     Get all activity fields
    /// </summary>
    /// <returns>Activity fields</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetAllActivityFieldsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var command = new GetAllActivityFieldsQueryRequest();

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}