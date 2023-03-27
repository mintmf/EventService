namespace EventService.Features.TicketFeature.CheckIfUserHasATicket
{
    /// <summary>
    /// Параметры метода проверки наличия у пользователя билета на заданное мероприятие
    /// </summary>
    public class CheckIfUserHasATicketParameters
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ID мероприятия
        /// </summary>
        public Guid EventId { get; set; }
    }
}
