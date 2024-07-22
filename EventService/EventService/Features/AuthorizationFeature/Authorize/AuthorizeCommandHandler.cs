using EventService.Services;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.AuthorizationFeature.Authorize;

/// <summary>
/// Класс обработчика команды аутентификации
/// </summary>
[UsedImplicitly]
public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, ScResult>
{
    private readonly IAuthorizationService _authorizationService;
        
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="authorizationService">Сервис авторизации</param>
    public AuthorizeCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    /// <summary>
    /// Обработчик команды аутентификации
    /// </summary>
    /// <param name="request">Команда авторизации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ответ по-умолчанию</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ScResult> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
    {
        return await _authorizationService.AuthorizeAsync();
    }
}