using EventService.Features.EventFeature;

namespace EventService.ObjectStorage;

/// <summary>
/// Интерфейс репозитория мероприятий
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Добавление мероприятия
    /// </summary>
    /// <param name="sourceEvent"></param>
    /// <returns></returns>
    Task<Event> AddEventAsync(Event sourceEvent);

    /// <summary>
    /// Изменение мероприятия
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="sourceEvent"></param>
    /// <returns></returns>
    Task<Event> UpdateEventAsync(Guid? eventId, Event sourceEvent);

    /// <summary>
    /// Удаление мероприятия
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    Task<bool> DeleteEventAsync(Guid eventId);

    /// <summary>
    /// Получение списка всех мероприятий
    /// </summary>
    /// <returns></returns>
    Task<List<Event>> GetEventListAsync();

    /// <summary>
    /// Удаление мероприятий
    /// </summary>
    /// <param name="spaceId"></param>
    /// <returns></returns>
    Task DeleteEventsBySpaceAsync(Guid spaceId);

    /// <summary>
    /// Удалить изображение
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    Task DeleteImageAsync(Guid imageId);

    /// <summary>
    /// Получить заданное мероприятие
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    Task<Event> GetEventAsync(Guid eventId);
}