using Launchpad.Application.Queries.Employees.GetOne;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeesController
{
    /// <summary>
    ///     Employee account details
    /// </summary>
    /// <returns>Employee data</returns>
    [Authorize(JwtDetailsRole.Curator)]
    [HttpGet("{employeeId:long}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployeesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(long employeeId)
    {
        var query = new GetOneEmployeesQueryRequest
        {
            Id = employeeId
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}