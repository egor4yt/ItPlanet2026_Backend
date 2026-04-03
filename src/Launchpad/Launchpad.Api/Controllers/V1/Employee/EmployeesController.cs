using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.Abstractions;
using Launchpad.Application.Commands.Employees.Update;
using Launchpad.Application.Commands.Employees.UpdateBiography;
using Launchpad.Application.Commands.Skills.AttachEmployee;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Queries.Employees.GetOne;
using Launchpad.Application.Queries.Employees.GetResponds;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployeesController
{
    /// <summary>
    ///     Authorized employee account details
    /// </summary>
    /// <returns>Employee data</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployeesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Me()
    {
        var query = new GetOneEmployeesQueryRequest
        {
            Id = CurrentUserService.ProfileId
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    ///     Update authorized employee skills
    /// </summary>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpPatch("skills")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateSkills([FromBody] UpdateEmployeeSkillsBody body)
    {
        var command = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            Skills = body.Skills.Select(x => new AttachEmployeeSkillsCommandRequestItem
            {
                Id = x.Id,
                Title = x.Title
            })
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Update authorized employee biography
    /// </summary>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpPatch("biography")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateBiography([FromBody] UpdateEmployeeBiographyBody body)
    {
        var command = new UpdateBiographyEmployeesCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            Biography = body.Biography
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Update employee details
    /// </summary>
    [Authorize(JwtDetailsRole.Employee)]
    [HttpPut]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateEmployeeBody body)
    {
        var command = new UpdateEmployeesCommandRequest
        {
            EmployeeId = CurrentUserService.ProfileId,
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName,
            IsMale = body.IsMale,
            BirthDate = body.BirthDate
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Search responds to vacancy
    /// </summary>
    [HttpGet("{employeeId:long}/responds")]
    [Authorize(JwtDetailsRole.Employee)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PagedResult<GetRespondsEmployeesQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SearchByEmployee([FromRoute] long employeeId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        if (CurrentUserService.ProfileId != employeeId && CurrentUserService.IsInRole(JwtDetailsRole.Employee))
            throw new ForbiddenException("UseYourProfileId");

        var query = new GetRespondsEmployeesQueryRequest
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}