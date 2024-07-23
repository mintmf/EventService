using EventService.Features.AuthorizationFeature.Authorize;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.AuthorizationFeature;

/// <summary>
/// Контроллер аутентификации
/// </summary>
[ApiController]
[Route("stub")]
[Authorize]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediatr;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthorizationController(IMediator mediator)
    {
        _mediatr = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Аутентификация
    /// </summary>
    /// <returns>Результат аутентификации</returns>
    /// <response code="200">Успех</response>
    /// <response code="401">Неуспех</response>
    [Route("authstub")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ScResult> Authorize()
    {
        return await _mediatr.Send(new AuthorizeCommand());
    }
}