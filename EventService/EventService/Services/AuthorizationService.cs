using SC.Internship.Common.ScResult;

namespace EventService.Services;

/// <summary>
/// Сервис авторизации
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <returns>Результат аутентификации</returns>
    public async Task<ScResult> AuthorizeAsync()
    {
        return await Task.FromResult(new ScResult());
    }
}