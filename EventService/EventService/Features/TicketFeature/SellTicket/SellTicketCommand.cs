using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.SellTicket
{
    /// <summary>
    /// Класс команды продажи пользователю билета
    /// </summary>
    public class SellTicketCommand : IRequest<ScResult>
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ID билета
        /// </summary>
        public Guid TicketId { get; set; }
    }
}
