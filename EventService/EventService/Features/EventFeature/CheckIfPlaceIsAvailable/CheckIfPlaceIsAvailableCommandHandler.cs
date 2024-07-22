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
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public CheckIfPlaceIsAvailableCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    /// <param name="command">Команда проверки доступности места</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Признак доступности</returns>
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

        return new ScResult<bool>
        {
            Result = true
        };
    }
}