using EventService.Features;

namespace EventService.Services
{
    /// <summary>
    /// Интерфейс сервиса платежей
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Создать платеж
        /// </summary>
        /// <returns></returns>
        Task<Payment> CreatePaymentAsync();

        /// <summary>
        /// Подтвердить платеж
        /// </summary>
        /// <returns></returns>
        /// <param name="paymentId"></param>
        Task<Payment> ConfirmPaymentAsync(Guid paymentId);

        /// <summary>
        /// Отменить платеж
        /// </summary>
        /// <returns></returns>
        /// <param name="paymentId"></param>
        Task<Payment> CancelPaymentAsync(Guid paymentId);
    }
}
