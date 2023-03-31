namespace EventService.Features;

/// <summary>
/// Событие RabbitMQ
/// </summary>
public class RabbitMqEvent
{
    /// <summary>
    /// Тип
    /// </summary>
    public RabbitMqEventType Type { get; set; }

    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }
}