using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.SellTicket
{
    /// <summary>
    /// Класс команды продажи пользователю билета
    /// </summary>
    public class SellTicketCommand : IRequest<ScResult>
    {
        public Guid UserId { get; set; }

        public Guid TicketId { get; set; }
    }
}
