using EventService.Features;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий платежей
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private List<Payment> _paymentList = new();

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns></returns>
        public Payment CreatePayment()
        {
            var payment = new Payment 
            { 
                PaymentId = Guid.NewGuid(),
                DateCreation = DateTime.Now
            };

            _paymentList.Add(payment);

            return payment;
        }
    }
}
