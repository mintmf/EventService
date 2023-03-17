using EventService.Features.EventFeature;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Интерфейс репозитория мероприятий
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Добавоение мероприятия
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
        Task<Event> UpdateEventAsync(Guid eventId, Event sourceEvent);

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
    }
}
