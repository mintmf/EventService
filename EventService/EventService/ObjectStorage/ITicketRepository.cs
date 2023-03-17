using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature;
using EventService.Features.TicketFeature.GiveUserATicket;
using SC.Internship.Common.ScResult;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<Ticket>> AddFreeTicketsAsync(AddFreeTicketsParameters parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Ticket> GiveUserAticketAsync(Guid userId, GiveUserATicketParameters? parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckIfUserHasATicket(Guid userId, Guid eventId);
    }
}
