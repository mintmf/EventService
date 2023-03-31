namespace EventService.ObjectStorage;

/// <summary>
/// Конфигурация для базы данных
/// </summary>
public class EventsMongoConfig
{
    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Имя базы данных
    /// </summary>
    public string? Database { get; set; }

    /// <summary>
    /// Имя коллекции
    /// </summary>
    public string? EventsCollection { get; set; }
}