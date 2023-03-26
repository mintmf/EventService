using EventService.Features.TicketFeature.GiveUserATicket;
using EventService.Infrastracture;
using EventService.ObjectStorage;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using Newtonsoft.Json;
using EventService.Services;

namespace EventService.Features.TicketFeature.SellTicket
{
    /// <summary>
    /// Класс обработчика команды продажи пользователю билета
    /// </summary>
    public class SellTicketCommandHandler : IRequestHandler<SellTicketCommand, ScResult>
    {
        private IPaymentRepository _paymentRepository;
        private ITicketRepository _ticketRepository;
        private HttpClient client = new ();
        private PaymentServiceConfig _config;
        private IPaymentService _paymentService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="options"></param>
        /// <param name="ticketRepository"></param>
        /// <param name="paymentService"></param>
        public SellTicketCommandHandler(IPaymentRepository payment, 
            IOptions<PaymentServiceConfig> options, ITicketRepository ticketRepository, IPaymentService paymentService)
        {
            _paymentRepository = payment;
            _config = options.Value;
            _ticketRepository = ticketRepository;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Обработчик команды продажи пользователю билета
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ScResult> Handle(SellTicketCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.CreatePaymentAsync();

            if (payment == null)
            {
                throw new ScException("Не удалось создать платеж");
            }

            try
            {
                var result = await _ticketRepository.GiveUserAticketAsync(
                    command.TicketId, new GiveUserATicketParameters { UserId = command.UserId });
            }
            catch (Exception ex)
            {
                //payment.PaymentState = PaymentState.Canceled;
                await _paymentService.CancelPaymentAsync(payment.PaymentId);

                throw new ScException(ex, "Ошибка при попытке передачи пользователю билета");
            }

            //payment.PaymentState = PaymentState.Confirmed;
            var res = await _paymentService.ConfirmPaymentAsync(payment.PaymentId);

            return new ScResult();
        }
    }
}
