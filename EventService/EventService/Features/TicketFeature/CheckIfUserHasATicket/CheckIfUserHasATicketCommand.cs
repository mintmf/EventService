using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.CheckIfUserHasATicket
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckIfUserHasATicketCommand : IRequest<ScResult<bool>>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EventId { get; set; }
    }
}
