using EventService.Features;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий платежей
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _paymentList;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="paymentList"></param>
        public PaymentRepository(List<Payment> paymentList)
        {
            _paymentList = paymentList;
        }

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
