using Launchpad.Application.Queries.WorkFormats.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class WorkFormatsController
{
    /// <summary>
    ///     Get all vacancy types
    /// </summary>
    /// <returns>Vacancy types</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetAllWorkFormatsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var query = new GetAllWorkFormatsQueryRequest();

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}