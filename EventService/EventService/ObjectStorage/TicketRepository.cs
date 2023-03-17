using EventService.Features.TicketFeature;
using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature.GiveUserATicket;
using SC.Internship.Common.Exceptions;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий билетов
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly List<Ticket> Tickets = new();
        
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventRepository"></param>
        public TicketRepository(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Добавить бесплатные билеты на мероприятие
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<Ticket>> AddFreeTicketsAsync(AddFreeTicketsParameters parameters)
        {
            var newTickets = new List<Ticket>();

            for (var i = 0; i < parameters.NumberOfTickets; i++)
            {
                newTickets.Add(new Ticket { Id = Guid.NewGuid() });
            }

            Tickets.AddRange(newTickets);

            await _eventRepository.AddTicketsToAnEventAsync(parameters.EventId, newTickets);

            return await Task.FromResult(newTickets);
        }

        /// <summary>
        /// Дать пользователю билет
        /// </summary>
        /// <returns></returns>
        public async Task<Ticket> GiveUserAticketAsync(Guid ticketId, GiveUserATicketParameters parameters)
        {
            var ticket = Tickets.Find(t => t.Id == ticketId);

            if (ticket == null)
            {
                throw new ScException("Такого билета не существует");
            }

            ticket.Owner = parameters.UserId;

            return await Task.FromResult(ticket);
        }

        /// <summary>
        /// Проверить, есть ли у пользователя билет на даннное мероприятие
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckIfUserHasATicket(Guid userId, Guid eventId)
        {
            var events = await _eventRepository.GetEventListAsync();

            var foundEvent = events.Find(e => e.EventId == eventId);

            if (foundEvent == null)
            {
                throw new ScException("Такого мероприятия не существует");
            }

            var ticketOwner = foundEvent.Tickets?.Find(t => t.Owner == userId);

            return ticketOwner != null;
        }
    }
}
