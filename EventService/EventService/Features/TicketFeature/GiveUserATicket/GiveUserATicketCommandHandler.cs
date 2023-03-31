﻿using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.GiveUserATicket
{
    /// <summary>
    /// Класс обработчика события выдачи пользователю билета
    /// </summary>
    [UsedImplicitly]
    public class GiveUserATicketCommandHandler : IRequestHandler<GiveUserATicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eveRepository"></param>
        public GiveUserATicketCommandHandler(IEventRepository eveRepository)
        {
            _eventRepository = eveRepository; 
        }

        /// <summary>
        /// Обработчик команды выдачи билета пользователю
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScResult<Ticket>> Handle(GiveUserATicketCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetEventListAsync();

            var foundEvent = events.Find(e => e.Tickets?.Find(t => t.Id == request.TicketId) != null);

            if (foundEvent?.Tickets == null)
            {
                throw new ScException("Мероприятия с таким билетом не существует");
            }

            var ticket = foundEvent.Tickets.Where(t => t.Id == request.TicketId).First();

            if (ticket == null)
            {
                throw new ScException("Такого билета не существует");
            }

            ticket.Owner = request.Parameters.UserId;

            await _eventRepository.UpdateEventAsync(foundEvent.EventId, foundEvent);

            return new ScResult<Ticket>
            {
                Result = ticket
            };
        }
    }
}
