using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable;

/// <summary>
/// Обработчик команды проверки места
/// </summary>
[UsedImplicitly]
public class CheckIfPlaceIsAvailableCommandHandler : IRequestHandler<CheckIfPlaceIsAvailableCommand, ScResult<bool>>
{
    private readonly IEventRepository _eventRepository;
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository"></param>
    public CheckIfPlaceIsAvailableCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ScResult<bool>> Handle(CheckIfPlaceIsAvailableCommand command, CancellationToken cancellationToken)
    {
        var foundEvent = await _eventRepository.GetEventAsync(command.EventId);

        if (foundEvent == null)
        {
            throw new ScException("Такого мероприятия не существует");
        }

        if (foundEvent.PlacesAvailable == false)
        {
            throw new ScException("У билетов для этого мероприятия нет мест");
        }

        if (foundEvent.Tickets == null)
        {
            throw new ScException("Для этого мероприятия нет билетов");
        }

        var foundTicket = foundEvent.Tickets.Find(p => p.Place == command.Place);

        if (foundTicket == null)
        {
            throw new ScException("Билета с таким местом не существует");
        }

        if (foundTicket.Owner != Guid.Empty)
        {
            return new ScResult<bool>
            {
                Result = false
            };
        }

        //var result = await _eventRepository.CheckIfPlaceIsAvailable(command.Place, command.EventId);

        return new ScResult<bool>
        {
            Result = true
        };
    }
}