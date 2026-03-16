using Launchpad.Api.Configuration.Options;
using Launchpad.Api.Contracts.Employee;
using Launchpad.Application.Commands.Employees.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Queries.Employees.GetOne;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Employees controller
/// </summary>
[Route("employees")]
public class EmployeesController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
    /// <summary>
    ///     Employee registration
    /// </summary>
    /// <param name="body">Registration employee data</param>
    /// <returns>Registered employee data and a JSON web token</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CreateEmployeeCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeBody body)
    {
        var command = new CreateEmployeeCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails(),
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName
        };

        var response = await Mediator.Send(command);

        return Created($"employee/{response.EmployeeId}", response);
    }

    /// <summary>
    ///     Employee account details
    /// </summary>
    /// <returns>Employee data</returns>
    [Authorize]
    [HttpGet("{employeeId:long}")]
    [ProducesResponseType(typeof(GetOneEmployeeQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(long employeeId)
    {
        if (CurrentUserService.UserId != employeeId)
            throw new NotFoundException("NotFound");

        var command = new GetOneEmployeeQueryRequest
        {
            Id = employeeId
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}