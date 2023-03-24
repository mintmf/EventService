using EventService.Features.TicketFeature.GiveUserATicket;
using EventService.ObjectStorage;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.SellTicket
{
    /// <summary>
    /// Класс обработчика команды продажи пользователю билета
    /// </summary>
    public class SellTicketCommandHandler : IRequestHandler<SellTicketCommand, ScResult>
    {
        private IPaymentRepository _paymentRepository;
        private ITicketRepository _ticketRepository;
        private IMediator _mediator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="payment"></param>
        public SellTicketCommandHandler(IPaymentRepository payment, IMediator mediator)
        {
            _paymentRepository = payment;
            _mediator = mediator;
        }

        /// <summary>
        /// Обработчик команды продажи пользователю билета
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ScResult> Handle(SellTicketCommand command, CancellationToken cancellationToken)
        {
            var payment = _paymentRepository.CreatePayment();
            payment.PaymentState = PaymentState.Hold;

            try
            {
                var result = await _ticketRepository.GiveUserAticketAsync(
                    command.TicketId, new GiveUserATicketParameters { UserId = command.UserId });
            }
            catch (Exception ex)
            {
                payment.PaymentState = PaymentState.Canceled;

                throw new ScException(ex, "Ошибка при попытке передачи пользователю билета");
            }

            payment.PaymentState = PaymentState.Confirmed;

            return new ScResult();
        }
    }
}
