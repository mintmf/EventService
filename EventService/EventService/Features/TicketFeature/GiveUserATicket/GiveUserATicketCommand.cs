using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.GiveUserATicket
{
    /// <summary>
    /// 
    /// </summary>
    public class GiveUserATicketCommand : IRequest<ScResult<Ticket>>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid TicketId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GiveUserATicketParameters Parameters {get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public GiveUserATicketCommand(GiveUserATicketParameters parameters)
        {
            Parameters = parameters;
        }
    }
}
