using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.Commands.Employees.Authorize;
using Launchpad.Application.Commands.Employees.Create;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeesController
{
    /// <summary>
    ///     Employee registration
    /// </summary>
    /// <param name="body">Registration employee data</param>
    /// <returns>Registered employee data and a JSON web token</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CreateEmployeesCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeBody body)
    {
        var command = new CreateEmployeesCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails(),
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName
        };

        var response = await Mediator.Send(command);

        return Created($"employees/{response.EmployeeId}", response);
    }

    /// <summary>
    ///     Employee authorization
    /// </summary>
    /// <returns>Employee data</returns>
    [AllowAnonymous]
    [HttpPost("authorization")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(AuthorizeEmployeeCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authorize([FromBody] AuthorizeEmployeeBody body)
    {
        var command = new AuthorizeEmployeeCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails()
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}