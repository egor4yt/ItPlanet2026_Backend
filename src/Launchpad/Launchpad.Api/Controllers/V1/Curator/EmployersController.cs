using Launchpad.Application.Abstractions;
using Launchpad.Application.Queries.Employers.Search;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace

namespace Launchpad.Api.Controllers.V1;

public partial class EmployersController
{
    /// <summary>
    ///     Search employers
    /// </summary>
    /// <returns>Employers data</returns>
    [HttpGet]
    [Authorize(JwtDetailsRole.Curator)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResult<SearchEmployersQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string? companyName, [FromQuery] List<int> verificationStatusId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var query = new SearchEmployersQueryRequest
        {
            Name = companyName,
            VerificationStatusId = verificationStatusId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}