using Asp.Versioning;
using Launchpad.Api.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Base API controller version 1.0
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    ///     Mediator instance in current HTTP request scope
    /// </summary>
    protected IMediator Mediator => field ??= HttpContext.RequestServices.GetService<IMediator>()!;

    /// <summary>
    ///     CurrentUserService instance in current HTTP request scope
    /// </summary>
    protected ICurrentUserService CurrentUserService => field ??= HttpContext.RequestServices.GetService<ICurrentUserService>()!;
}