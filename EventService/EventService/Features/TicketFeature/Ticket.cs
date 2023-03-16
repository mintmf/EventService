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
        public string? Place { get; set; }
    }
}
