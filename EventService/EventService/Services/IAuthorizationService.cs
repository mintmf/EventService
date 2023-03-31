using SC.Internship.Common.ScResult;

namespace EventService.Services;

/// <summary>
/// 
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<ScResult> AuthorizeAsync();
}