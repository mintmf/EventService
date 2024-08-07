﻿using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.AddFreeTickets;

/// <summary>
/// Обработчик команды добавления бесплатных билетов
/// </summary>
[UsedImplicitly]
public class AddFreeTicketsCommandHandler : IRequestHandler<AddFreeTicketsCommand, ScResult<List<Ticket>>>
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public AddFreeTicketsCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    /// <summary>
    /// Обработчик команды добавления бесплатных билетов
    /// </summary>
    /// <param name="request">Команда добавления бесплатных билетов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список билетов</returns>
    public async Task<ScResult<List<Ticket>>> Handle(AddFreeTicketsCommand request, CancellationToken cancellationToken)
    {
        var result = await _eventRepository.GetEventAsync(request.Parameters.EventId);

        if (result == null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        var newTickets = new List<Ticket>();

        for (var i = 0; i < request.Parameters.NumberOfTickets; i++)
        {
            newTickets.Add(result.PlacesAvailable
                ? new Ticket { Id = Guid.NewGuid(), Place = i + 1 }
                : new Ticket { Id = Guid.NewGuid() });
        }

        result.Tickets = newTickets;

        await _eventRepository.UpdateEventAsync(request.Parameters.EventId, result);

        return new ScResult<List<Ticket>>
        {
            Result = result.Tickets
        };
    }
}