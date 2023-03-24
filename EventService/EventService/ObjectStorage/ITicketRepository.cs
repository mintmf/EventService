using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature;
using EventService.Features.TicketFeature.GiveUserATicket;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий билетов
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Добаить бесплатные билеты на мероприятие
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<Ticket>> AddFreeTicketsAsync(AddFreeTicketsParameters parameters);

        /// <summary>
        /// Выдать пользователю билет
        /// </summary>
        /// <returns></returns>
        Task<Ticket> GiveUserAticketAsync(Guid userId, GiveUserATicketParameters parameters);

        /// <summary>
        /// Проверить, есть ли у пользователя билет
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckIfUserHasATicket(Guid userId, Guid eventId);
    }
}
