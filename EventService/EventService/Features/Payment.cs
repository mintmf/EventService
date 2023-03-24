namespace EventService.Features
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
        /// Состояние платеда
        /// </summary>
        public enum State
        {
        }

        /// <summary>
        /// Дата создания платежа
        /// </summary>
        public DateTime DateCreation { get; set; }
        
        /// <summary>
        /// Дата подтверждения плтежа 
        /// </summary>
        public DateTime DateConfirmation { get; set; }
        
        /// <summary>
        /// Дата отмены платежа
        /// </summary>
        public DateTime DateConcellation { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
