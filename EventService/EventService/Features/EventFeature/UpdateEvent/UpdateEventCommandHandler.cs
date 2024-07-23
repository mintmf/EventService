using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;

namespace EventService.Features.EventFeature.UpdateEvent;

/// <summary>
/// Класс обработчика команды изменения мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    /// <summary>
    /// Обработчик команды изменения мероприятия
    /// </summary>
    /// <param name="command">Команда обновления мероприятия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Мероприятие</returns>
    public async Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
    {
        command.Event.EventId = command.EventId;

        var result = await _eventRepository.UpdateEventAsync(command.EventId, command.Event);

        return await Task.FromResult(result);
    }
}