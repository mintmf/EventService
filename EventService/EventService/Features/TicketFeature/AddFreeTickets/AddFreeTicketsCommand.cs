using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.AddFreeTickets
{
    /// <summary>
    /// Добавление бесплатных билетов
    /// </summary>
    public class AddFreeTicketsCommand : IRequest<ScResult<List<Ticket>>>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public AddFreeTicketsCommand()
        {
            Parameters = new AddFreeTicketsParameters();
        }

        /// <summary>
        /// Параметры
        /// </summary>
        public AddFreeTicketsParameters Parameters { get; set; }
    }
}
