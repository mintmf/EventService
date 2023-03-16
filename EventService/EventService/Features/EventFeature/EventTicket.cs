namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Билет на мероприятие
    /// </summary>
    public class EventTicket
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
