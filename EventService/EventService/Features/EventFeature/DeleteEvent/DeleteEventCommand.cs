using MediatR;

namespace EventService.Features.EventFeature.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public Guid EventId { get; set; }

        public DeleteEventCommand(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
