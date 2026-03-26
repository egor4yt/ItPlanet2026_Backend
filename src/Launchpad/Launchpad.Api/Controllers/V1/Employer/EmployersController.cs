using Launchpad.Api.Contracts.Employers;
using Launchpad.Application.Abstractions;
using Launchpad.Application.Commands.Employers.Update;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Queries.Employers.GetOne;
using Launchpad.Application.Queries.Employers.GetResponds;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class EmployersController
{
    /// <summary>
    ///     Get employer details
    /// </summary>
    /// <returns>Employer data</returns>
    [Authorize(JwtDetailsRole.Employer)]
    [HttpGet("{employerId:long}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneEmployersQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetOne([FromRoute] long employerId)
    {
        if (CurrentUserService.IsInRole(JwtDetailsRole.Employer) && employerId != CurrentUserService.ProfileId)
            throw new NotFoundException("EmployerNotFound");

        var query = new GetOneEmployersQueryRequest
        {
            Id = employerId
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    ///     Update employer details
    /// </summary>
    [Authorize(JwtDetailsRole.Employer)]
    [HttpPut("{employerId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] long employerId, [FromBody] UpdateEmployerDescriptionBody body)
    {
        if (CurrentUserService.IsInRole(JwtDetailsRole.Employer) && employerId != CurrentUserService.ProfileId)
            throw new NotFoundException("EmployerNotFound");

        var command = new UpdateEmployersCommandRequest
        {
            EmployerId = employerId,
            Description = body.Description,
            ActivityFieldIds = body.ActivityFieldIds ?? []
        };

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    ///     Search responds to vacancy
    /// </summary>
    [HttpGet("{employerId:long}/{vacancyId:long}/responds")]
    [Authorize(JwtDetailsRole.Employer)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PagedResult<GetRespondsEmployersQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SearchByEmployee([FromRoute] long employerId, [FromRoute] long vacancyId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        if (CurrentUserService.ProfileId != employerId && CurrentUserService.IsInRole(JwtDetailsRole.Employer))
            throw new ForbiddenException("UseYourProfileId");

        var query = new GetRespondsEmployersQueryRequest
        {
            EmployerId = employerId,
            VacancyId = vacancyId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}