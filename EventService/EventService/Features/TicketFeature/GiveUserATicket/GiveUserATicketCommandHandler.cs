using EventService.ObjectStorage;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.GiveUserATicket
{
    /// <summary>
    /// 
    /// </summary>
    public class GiveUserATicketCommandHandler : IRequestHandler<GiveUserATicketCommand, ScResult<Ticket>>
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ticketRepository"></param>
        public GiveUserATicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Обработчик команды выдачи билета пользователю
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScResult<Ticket>> Handle(GiveUserATicketCommand request, CancellationToken cancellationToken)
        {
            var result = await _ticketRepository.GiveUserAticketAsync(request.TicketId, request.Parameters);

            return new ScResult<Ticket>
            {
                Result = result
            };
        }
    }
}
