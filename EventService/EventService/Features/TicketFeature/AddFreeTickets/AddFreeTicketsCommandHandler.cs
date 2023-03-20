using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.AddFreeTickets
{
    /// <summary>
    /// Обработчик команды добавления бесплатных билетов
    /// </summary>
    [UsedImplicitly]
    public class AddFreeTicketsCommandHandler : IRequestHandler<AddFreeTicketsCommand, ScResult<List<Ticket>>>
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ticketRepository"></param>
        public AddFreeTicketsCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Обработчик команды добавления бесплтных билетов
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ScResult<List<Ticket>>> Handle(AddFreeTicketsCommand request, CancellationToken cancellationToken)
        {
            var result = await _ticketRepository.AddFreeTicketsAsync(request.Parameters);
            return new ScResult<List<Ticket>>
            {
                Result = result
            };
        }
    }
}
