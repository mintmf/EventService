using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CreateEvent;

/// <summary>
/// Команда создания мероприятия
/// </summary>
public class CreateEventCommand : IRequest<ScResult<Event>>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public CreateEventCommand()
    {
        Event = new Event();
    }

    /// <summary>
    /// Мероприятияе
    /// </summary>
    public Event Event { get; set; }
}