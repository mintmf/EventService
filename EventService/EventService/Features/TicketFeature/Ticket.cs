using JetBrains.Annotations;

namespace EventService.Features.TicketFeature
{
    /// <summary>
    /// Билет на мероприятие
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// ID билета
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Владелец билета
        /// </summary>
        public Guid Owner { get; set; }

        /// <summary>
        /// Место (опционально)
        /// </summary>

        [UsedImplicitly]
        public int Place { get; set; }
    }
}
