using Launchpad.Api.Contracts.Vacancies;
using Launchpad.Application.Abstractions;
using Launchpad.Application.Queries.Vacancies.GetOne;
using Launchpad.Application.Queries.Vacancies.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

public partial class VacanciesController
{
    /// <summary>
    ///     Search vacancies
    /// </summary>
    [AllowAnonymous]
    [HttpPost("search")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResult<SearchVacanciesQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromBody] SearchVacancyBody body)
    {
        var query = new SearchVacanciesQueryRequest
        {
            Title = body.Title?.Trim(),
            IncludeIds = body.IncludeIds,
            RadiusSearch = body.RadiusSearch?.ToApplicationModel(),
            BoxSearch = body.BoxSearch?.ToApplicationModel(),
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    ///     Get vacancy by id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{vacancyId:long}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GetOneVacanciesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] long vacancyId)
    {
        var query = new GetOneVacanciesQueryRequest
        {
            Id = vacancyId
        };

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}