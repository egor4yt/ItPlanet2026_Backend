using Launchpad.Api.Configuration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Launchpad.Api.Controllers.V1;

/// <summary>
///     Employees controller
/// </summary>
[Route("employees")]
public partial class EmployeesController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
}