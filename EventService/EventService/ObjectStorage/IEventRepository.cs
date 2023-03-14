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

        Task<Event> UpdateEventAsync(Guid eventId, Event sourceEvent);

        Task<bool> DeleteEventAsync(Guid eventId);

        Task<List<Event>> GetEventListAsync();
    }
}
