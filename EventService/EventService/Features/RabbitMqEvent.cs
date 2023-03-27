namespace EventService.Features
{
    public class RabbitMqEvent
    {
        public RabbitMqEventType Type { get; set; }

        public Guid Id { get; set; }
    }
}
