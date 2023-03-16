using EventService.Features.AuthorizationFeature.Authorize;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.AuthorizationFeature
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("stub")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediatr;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public AuthorizationController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>401</returns>
        [Route("authstub")]
        [HttpGet]
        [Authorize]
        public async Task<ScResult> Authorize()
        {
            return await _mediatr.Send(new AuthorizeCommand());
        }
    }
}
