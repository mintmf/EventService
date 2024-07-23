namespace PaymentService.ObjectStorage
{
    /// <summary>
    /// Репозиторий платежей
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns>Платеж</returns>
        public Payment CreatePayment();
        
        /// <summary>
        /// Подтверждение платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        public Payment ConfirmPayment(Guid paymentId);

        /// <summary>
        /// Отмена платежа
        /// </summary>
        /// <param name="paymentId">ID платежа</param>
        /// <returns>Платеж</returns>
        public Payment CancelPayment(Guid paymentId);
    }
}
