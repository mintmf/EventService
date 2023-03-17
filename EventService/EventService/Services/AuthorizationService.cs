using EventService.Models.Configs;
using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IOptions<IdentityServerConfig> _config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AuthorizationService(IOptions<IdentityServerConfig> config)
        {
            _config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ScResult> AuthorizeAsync()
        {
            return new ScResult { };
        }
    }
}
