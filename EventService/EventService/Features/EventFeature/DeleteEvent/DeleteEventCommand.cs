using MediatR;

namespace EventService.Features.EventFeature.DeleteEvent
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEventCommand : IRequest
    {
        /// <summary>
        /// ID мероприятия
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventId"></param>
        public DeleteEventCommand(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
