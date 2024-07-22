using EventService.ObjectStorage;
using EventService.Services;
using JetBrains.Annotations;
using MediatR;

namespace EventService.Features.EventFeature.DeleteEvent;

/// <summary>
/// Обработчик команды удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    private readonly IRabbitMqService _rabbitMqService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    /// <param name="rabbitMqService">Сервис RabbitMq</param>
    public DeleteEventCommandHandler(IEventRepository eventRepository, IRabbitMqService rabbitMqService)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _rabbitMqService = rabbitMqService ?? throw new ArgumentNullException(nameof(rabbitMqService));
    }

    /// <summary>
    /// Обработчик команды удаления мероприятия
    /// </summary>
    /// <param name="command">Команда удаления мероприятия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задание</returns>
    public async Task Handle(DeleteEventCommand command, CancellationToken cancellationToken)
    {
        await _eventRepository.DeleteEventAsync(command.EventId);

        var rabbitEventDeleteEvent = new RabbitMqEvent 
        { 
            Id = command.EventId, 
            Type = RabbitMqEventType.EventDelete
        };

        _rabbitMqService.SendMessage(rabbitEventDeleteEvent);
    }
}