using Launchpad.Candidates.Api.Extensions;
using Launchpad.Candidates.Application.Commands.Candidates.Create;
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
    /// <returns>Skills</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(CreateCandidatesCommandResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create()
    {
        var command = new CreateCandidatesCommandRequest
        {
            KeycloakId = CurrentUserService.IdentityId
        };

        var response = await Mediator.Send(command);
        return response.ToActionResult(StatusCodes.Status201Created);
    }

    /// <summary>
    ///     Get authorized candidate profile
    /// </summary>
    /// <returns>Skills</returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(GetOneCandidatesQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me()
    {
        var query = new GetOneCandidatesQueryRequest
        {
            KeycloakId = CurrentUserService.IdentityId
        };

        var response = await Mediator.Send(query);
        return response.ToActionResult(StatusCodes.Status200OK);
    }
}