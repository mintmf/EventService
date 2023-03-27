using EventService.Features;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Интерфейс репозитория платежей
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global сейчас не используется
        public Payment CreatePayment();
    }
}
