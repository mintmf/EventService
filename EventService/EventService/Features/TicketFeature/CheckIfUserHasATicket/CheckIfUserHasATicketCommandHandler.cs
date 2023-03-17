using EventService.ObjectStorage;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.CheckIfUserHasATicket
{
    /// <summary>
    /// Класс обработчика команды проверки на то, есть ли у пользователя билет
    /// </summary>
    public class CheckIfUserHasATicketCommandHandler : IRequestHandler<CheckIfUserHasATicketCommand, ScResult<bool>>
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ticketRepository"></param>
        public CheckIfUserHasATicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Обработчик команды проверки на то, есть ли у пользователя билет
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScResult<bool>> Handle(CheckIfUserHasATicketCommand request, CancellationToken cancellationToken)
        {
            var result = await _ticketRepository.CheckIfUserHasATicket(request.UserId, request.EventId);

            return new ScResult<bool>
            {
                Result = result
            };
        }
    }
}
