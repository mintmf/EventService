using MediatR;

namespace EventService.Features.EventFeature.UpdateEvent;

/// <summary>
/// Класс команды изменения мероприятия
/// </summary>
public class UpdateEventCommand : IRequest<Event>
{
    /// <summary>
    /// ID мероприятия
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Мероприятие
    /// </summary>
    public Event Event { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="eventSource"></param>
    public UpdateEventCommand(Guid eventId, Event eventSource)
    {
        EventId = eventId;
        Event = eventSource;
    }
}