using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CreateEvent;

/// <summary>
/// Класс обработчика команды создания нового мероприятия
/// </summary>
[UsedImplicitly]
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное мероприятие</returns>
    public async Task<ScResult<Event>> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        command.Event.EventId = Guid.NewGuid();

        var createdEvent = await _eventRepository.AddEventAsync(command.Event);

        return new ScResult<Event>
        {
            Result = createdEvent
        };
    }
}