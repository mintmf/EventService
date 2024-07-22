using EventService.ObjectStorage;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using EventService.Services;
using JetBrains.Annotations;

namespace EventService.Features.TicketFeature.SellTicket;

/// <summary>
/// Класс обработчика команды продажи пользователю билета
/// </summary>
[UsedImplicitly]
public class SellTicketCommandHandler : IRequestHandler<SellTicketCommand, ScResult>
{
    private readonly IPaymentService _paymentService;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="paymentService">Сервис платежей</param>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public SellTicketCommandHandler(IPaymentService paymentService, IEventRepository eventRepository)
    {
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository)); 
    }

    /// <summary>
    /// Обработчик команды продажи пользователю билета
    /// </summary>
    /// <param name="command">Команда продажи пользователю билета</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат по-умолчанию</returns>
    public async Task<ScResult> Handle(SellTicketCommand command, CancellationToken cancellationToken)
    {
        var payment = await _paymentService.CreatePaymentAsync();

        if (payment == null)
        {
            throw new ScException("Не удалось создать платеж");
        }

        try
        {
            var events = await _eventRepository.GetEventListAsync();

            var foundEvent = events.Find(e => e.Tickets?.Find(t => t.Id == command.TicketId) != null);

            if (foundEvent?.Tickets == null)
            {
                throw new ScException("Мероприятия с таким билетом не существует");
            }

            var ticket = foundEvent.Tickets.First(t => t.Id == command.TicketId);

            if (ticket == null)
            {
                throw new ScException("Такого билета не существует");
            }

            ticket.Owner = command.UserId;

            await _eventRepository.UpdateEventAsync(foundEvent.EventId, foundEvent);
        }
        catch (Exception ex)
        {
            await _paymentService.CancelPaymentAsync(payment.PaymentId);

            throw new ScException(ex, "Ошибка при попытке передачи пользователю билета");
        }

        await _paymentService.ConfirmPaymentAsync(payment.PaymentId);

        return new ScResult();
    }
}