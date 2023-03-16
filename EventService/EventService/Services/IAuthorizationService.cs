using SC.Internship.Common.ScResult;

namespace EventService.Services
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ScResult> AuthorizeAsync();
    }
}
