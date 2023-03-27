namespace PaymentService
{
    /// <summary>
    /// Репозиторий платежей
    /// </summary>
    public class PaymentRepository
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
                DateCreation = DateTime.Now
            };

            _paymentList.Add(payment);

            return payment;
        }

        public Payment ConfirmPayment(Guid paymentId)
        {
            var index = _paymentList.FindIndex(p => p.PaymentId == paymentId);

            _paymentList[index].PaymentState = PaymentState.Confirmed;

            return _paymentList[index];
        }

        public Payment CancelPayment(Guid paymentId)
        {
            var index = _paymentList.FindIndex(p => p.PaymentId == paymentId);

            _paymentList[index].PaymentState = PaymentState.Canceled;

            return _paymentList[index];
        }
    }
}
