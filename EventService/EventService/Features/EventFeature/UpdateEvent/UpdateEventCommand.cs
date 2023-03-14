using MediatR;

namespace EventService.Features.EventFeature.UpdateEvent
{
    public class UpdateEventCommand : IRequest<Event>
    {
        public Guid EventId { get; set; }

        public Event Event { get; set; }

        public UpdateEventCommand(Guid eventId, Event eventSource)
        {
            EventId = eventId;
            Event = eventSource;
        }
    }
}
