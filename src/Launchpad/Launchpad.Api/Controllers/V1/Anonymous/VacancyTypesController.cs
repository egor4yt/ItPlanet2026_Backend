using Launchpad.Application.Queries.VacancyTypes.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class VacancyTypesController
{
    /// <summary>
    ///     Get all vacancy types
    /// </summary>
    /// <returns>Vacancy types</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetAllVacancyTypesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GelAll()
    {
        var query = new GetAllVacancyTypesQueryRequest();

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}