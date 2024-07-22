using SC.Internship.Common.ScResult;

namespace EventService.Services;

/// <summary>
/// Сервис авторизации
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Проверка авторизации
    /// </summary>
    /// <returns></returns>
    Task<ScResult> AuthorizeAsync();
}