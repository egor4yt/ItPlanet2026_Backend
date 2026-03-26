using Launchpad.Api.Contracts.Vacancies;
using Launchpad.Application.Commands.Vacancies.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Queries.EmployerVerifications.Action;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class VacanciesController
{
    /// <summary>
    ///     Create vacancy
    /// </summary>
    /// <param name="employerId">Employer ID</param>
    /// <param name="body">Body</param>
    [HttpPost("{employerId:long}")]
    [Authorize(JwtDetailsRole.Employer)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(GetOneEmployerVerificationsQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromRoute] long employerId, [FromBody] CreateVacancyBody body)
    {
        if (CurrentUserService.ProfileId != employerId && CurrentUserService.IsInRole(JwtDetailsRole.Employer))
            throw new ForbiddenException("UseYourProfileId");

        var query = new CreateVacanciesCommandRequest
        {
            EmployerId = employerId,
            Title = body.Title,
            StartDate = body.StartDate,
            EndDate = body.EndDate,
            Description = body.Description,
            City = body.City,
            FullAddress = body.FullAddress,
            Location = body.Location.ToApplicationModel(),
            WorkFormatIds = body.WorkFormatIds,
            TypeId = body.TypeId,
            Skills = body.Skills.Select(x => new CreateVacanciesCommandRequestSkill
            {
                Id = x.Id,
                Title = x.Title
            })
        };

        var response = await Mediator.Send(query);

        return Created($"vacancy/{response.VacancyId}", response);
    }
}