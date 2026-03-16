using Launchpad.Api.Configuration.Options;
using Launchpad.Api.Contracts.Employee;
using Launchpad.Application.Commands.Users.Create;
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
    ///     User registration
    /// </summary>
    /// <param name="body">Registration user data</param>
    /// <returns>Registered user data and a JSON web token</returns>
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

        return Created($"user/{response.EmployeeId}", response);
    }
}