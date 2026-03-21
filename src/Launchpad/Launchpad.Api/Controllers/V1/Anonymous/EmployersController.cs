using Launchpad.Api.Contracts.Employers;
using Launchpad.Application.Commands.Employers.Authorize;
using Launchpad.Application.Commands.Employers.Create;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployersController
{
    /// <summary>
    ///     Employer registration
    /// </summary>
    /// <param name="body">Registration employer data</param>
    /// <returns>Registered employer data and a JSON web token</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CreateEmployersCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployersBody body)
    {
        var command = new CreateEmployersCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails(),
            CompanyName = body.CompanyName
        };

        var response = await Mediator.Send(command);

        return Created("employers", response);
    }

    /// <summary>
    ///     Employer authorization
    /// </summary>
    /// <returns>Employer data</returns>
    [AllowAnonymous]
    [HttpPost("authorization")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(AuthorizeEmployersCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authorize([FromBody] AuthorizeEmployerBody body)
    {
        var command = new AuthorizeEmployersCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails()
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}