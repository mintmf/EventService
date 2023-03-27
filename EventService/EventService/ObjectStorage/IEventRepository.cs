using EventService.Features.EventFeature;
using EventService.Features.TicketFeature;

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

        /// <summary>
        /// Добавить бесплатные билеты на мероприятие
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="tickets"></param>
        /// <returns></returns>
        Task AddTicketsToAnEventAsync(Guid eventId, List<Ticket> tickets);

        /// <summary>
        /// Проверить номер места на мероприятие
        /// </summary>
        /// <param name="place"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task<bool> CheckIfPlaceIsAvailable(int place, Guid eventId);

        /// <summary>
        /// Удаление мерроприятий
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
    }
}
