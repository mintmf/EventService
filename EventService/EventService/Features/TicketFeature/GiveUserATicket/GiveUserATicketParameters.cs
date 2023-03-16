namespace EventService.Features.TicketFeature.GiveUserATicket
{
    /// <summary>
    /// Параметры метода, который выдает пользователю билет
    /// </summary>
    public class GiveUserATicketParameters
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
