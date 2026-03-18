using Launchpad.Api.Configuration.Options;
using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.Commands.Employees.Authorize;
using Launchpad.Application.Commands.Employees.Create;
using Launchpad.Application.Commands.Employees.UpdateBiography;
using Launchpad.Application.Commands.Skills.AttachEmployee;
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
    ///     Employee account details
    /// </summary>
    /// <returns>Employee data</returns>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpGet("{employeeId:long}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployeesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(long employeeId)
    {
        if (CurrentUserService.ProfileId != employeeId)
            throw new NotFoundException("NotFound");

        var command = new GetOneEmployeesQueryRequest
        {
            Id = employeeId
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    ///     Authorized employee account details
    /// </summary>
    /// <returns>Employee data</returns>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpGet]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployeesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Me()
    {
        var command = new GetOneEmployeesQueryRequest
        {
            Id = CurrentUserService.ProfileId
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    ///     Update authorized employee skills
    /// </summary>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpPatch("skills")]
    [ProducesResponseType(typeof(AttachEmployeeSkillsCommandResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateSkills([FromBody] UpdateEmployeeSkillsBody body)
    {
        var command = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            Skills = body.Skills.Select(x => new AttachEmployeeSkillsCommandRequestItem
            {
                SkillId = x.SkillId,
                Title = x.Title
            })
        };

        _ = await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Update authorized employee biography
    /// </summary>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpPatch("biography")]
    [ProducesResponseType(typeof(UpdateBiographyEmployeesCommandResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateBiography([FromBody] UpdateEmployeeBiographyBody body)
    {
        var command = new UpdateBiographyEmployeesCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            Biography = body.Biography
        };

        _ = await Mediator.Send(command);

        return NoContent();
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