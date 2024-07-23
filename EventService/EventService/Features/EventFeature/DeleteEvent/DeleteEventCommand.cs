using MediatR;

namespace EventService.Features.EventFeature.DeleteEvent;

/// <summary>
/// Команда удаления мероприятия
/// </summary>
public class DeleteEventCommand : IRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventId"></param>
    public DeleteEventCommand(Guid eventId)
    {
        EventId = eventId;
    }

    /// <summary>
    /// ID мероприятия
    /// </summary>
    public Guid EventId { get; set; }
}