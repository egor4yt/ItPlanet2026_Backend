using Launchpad.Candidates.Api.Extensions;
using Launchpad.Candidates.Application.Commands.Candidates.Create;
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
}