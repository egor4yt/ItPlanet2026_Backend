using Launchpad.Candidates.Api.Contracts.Candidates;
using Launchpad.Candidates.Api.Extensions;
using Launchpad.Candidates.Application.Commands.Candidates.Create;
using Launchpad.Candidates.Application.Commands.Candidates.Update;
using Launchpad.Candidates.Application.Commands.Candidates.UpdateSkills;
using Launchpad.Candidates.Application.Queries.Candidates.GetOne;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Candidates.Api.Controllers.V1;

public partial class CandidatesController
{
    /// <summary>
    ///     Create candidate profile
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(CreateCandidatesCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateCandidatesBody body)
    {
        var command = new CreateCandidatesCommandRequest
        {
            KeycloakId = CurrentUserService.IdentityId,
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName
        };

        var response = await Mediator.Send(command);
        return response.ToActionResult(StatusCodes.Status201Created);
    }

    /// <summary>
    ///     Get authorized candidate profile
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(GetOneCandidatesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe()
    {
        var query = new GetOneCandidatesQueryRequest
        {
            KeycloakId = CurrentUserService.IdentityId
        };

        var response = await Mediator.Send(query);
        return response.ToActionResult(StatusCodes.Status200OK);
    }

    /// <summary>
    ///     Update authorized candidate profile
    /// </summary>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateCandidatesBody body)
    {
        var command = new UpdateCandidatesCommandRequest
        {
            KeycloakId = CurrentUserService.IdentityId,
            FirstName = body.FirstName,
            LastName = body.LastName,
            MiddleName = body.MiddleName,
            Biography = body.Biography,
            Birthdate = body.Birthdate
        };

        var response = await Mediator.Send(command);
        return response.ToActionResult(StatusCodes.Status204NoContent);
    }

    /// <summary>
    ///     Update authorized candidate skills
    /// </summary>
    [HttpPut("skills")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMySkills([FromBody] UpdateCandidatesSkillsBody body)
    {
        var command = new UpdateSkillsCandidatesCommandRequest
        {
            KeycloakId = CurrentUserService.IdentityId,
            Skills = body.Skills.Select(x => new UpdateSkillsCandidatesCommandRequestSkill
            {
                Id = x.Id,
                Title = x.Title
            })
        };

        var response = await Mediator.Send(command);
        return response.ToActionResult(StatusCodes.Status204NoContent);
    }
}