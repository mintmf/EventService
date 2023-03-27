namespace EventService.Infrastracture
{
    /// <summary>
    /// Конфигурация RabbitMQ
    /// </summary>
    public class RabbitMqConfig
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Имя очереди
        /// </summary>
        public string? QueueName { get; set; }
    }
}
