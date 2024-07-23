namespace PaymentService.ObjectStorage
{
    /// <summary>
    /// Репозиторий платежей
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private static List<Payment> _paymentList = new();

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns></returns>
        public Payment CreatePayment()
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                DateCreation = DateTimeOffset.Now
            };

            _paymentList.Add(payment);

            return payment;
        }

        /// <summary>
        /// Подтверждение платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        public Payment ConfirmPayment(Guid paymentId)
        {
            var index = _paymentList.FindIndex(p => p.PaymentId == paymentId);

            _paymentList[index].PaymentState = PaymentState.Confirmed;

            return _paymentList[index];
        }

        /// <summary>
        /// Отмена платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        public Payment CancelPayment(Guid paymentId)
        {
            var index = _paymentList.FindIndex(p => p.PaymentId == paymentId);

            _paymentList[index].PaymentState = PaymentState.Canceled;

            return _paymentList[index];
        }
    }
}
