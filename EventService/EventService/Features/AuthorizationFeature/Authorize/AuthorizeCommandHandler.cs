using EventService.Services;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.AuthorizationFeature.Authorize
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, ScResult>
    {
        private readonly IAuthorizationService _authorizationService;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="authorizationService"></param>
        public AuthorizeCommandHandler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScResult> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            return await _authorizationService.AuthorizeAsync();
        }
    }
}
