namespace EventService.Features;

/// <summary>
/// Тип сообщения
/// </summary>
public enum RabbitMqEventType
{
    /// <summary>
    /// Удалить пространство
    /// </summary>
    SpaceDelete = 1,
    /// <summary>
    /// Удалить изображение
    /// </summary>
    ImageDelete = 2,
    /// <summary>
    /// Удалить мероприятие
    /// </summary>
    EventDelete = 3
}