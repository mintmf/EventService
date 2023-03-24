namespace EventService.Features
{
    /// <summary>
    /// Состояние платежа
    /// </summary>
    public enum PaymentState
    {
        /// <summary>
        /// Ожидание выполнения операции передачи билета пользователю
        /// </summary>
        Hold,

        /// <summary>
        /// Подвтерждение платежа
        /// </summary>
        Confirmed,

        /// <summary>
        /// Отмена платежа
        /// </summary>
        Canceled
    }
}
