namespace EventService.Infrastracture
{
    /// <summary>
    /// Конфигурация сервиса платежей
    /// </summary>
    public class PaymentServiceConfig
    {
        /// <summary>
        /// Адрес сервиса
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Адрес метода создания платежа
        /// </summary>
        public string? CreatePaymentEndpoint { get; set; }

        /// <summary>
        /// Адрес метода подтверждения платежа
        /// </summary>
        public string? ConfirmPaymentEndpoint { get; set; }

        /// <summary>
        /// Адрсе метода отмены платежа
        /// </summary>
        public string? CancelPaymentEndpoint { get; set; }
    }
}
