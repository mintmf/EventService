namespace EventService.Services;

/// <summary>
/// Сервис RabbitMq
/// </summary>
public interface IRabbitMqService
{
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="obj"></param>
    void SendMessage(object obj);

    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="message"></param>
    // ReSharper disable once UnusedMemberInSuper.Global сейчас не используется
    void SendMessage(string message);
}