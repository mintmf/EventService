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
    /// <param name="eventRepository"></param>
    /// <param name="rabbitMqService"></param>
    public DeleteEventCommandHandler(IEventRepository eventRepository, IRabbitMqService rabbitMqService)
    {
        _eventRepository = eventRepository;
        _rabbitMqService = rabbitMqService;
    }

    /// <summary>
    /// Обработчик команды удаления мероприятия
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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