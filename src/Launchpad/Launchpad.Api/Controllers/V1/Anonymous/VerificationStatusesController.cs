using Launchpad.Application.Queries.VerificationStatuses.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class VerificationStatusesController
{
    /// <summary>
    ///     Get all verification statuses
    /// </summary>
    /// <returns>Verification statuses</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetAllVerificationStatusesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var query = new GetAllVerificationStatusesQueryRequest();

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}