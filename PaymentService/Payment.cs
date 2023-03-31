namespace PaymentService
{
    /// <summary>
    /// Класс платежной операции
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// ID платежа
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Состояние платежа
        /// </summary>
        public PaymentState PaymentState { get; set; }

        /// <summary>
        /// Дата создания платежа
        /// </summary>
        public DateTimeOffset DateCreation { get; set; }
        
        /// <summary>
        /// Дата подтверждения плтежа 
        /// </summary>
        public DateTimeOffset DateConfirmation { get; set; }
        
        /// <summary>
        /// Дата отмены платежа
        /// </summary>
        public DateTimeOffset DateConcellation { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
