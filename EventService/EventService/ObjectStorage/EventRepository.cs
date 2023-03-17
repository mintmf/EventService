using EventService.Features.EventFeature;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий мероприятий
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private static readonly List<Event> Events = new();

        /// <summary>
        /// Добавление мероприятия
        /// </summary>
        /// <param name="sourceEvent"></param>
        /// <returns></returns>
        public async Task<Event> AddEventAsync(Event sourceEvent)
        {
            sourceEvent.EventId = Guid.NewGuid();

            Events.Add(sourceEvent);

            return await Task.FromResult(sourceEvent);
        }

        /// <summary>
        /// Изменение мероприятия
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="sourceEvent"></param>
        /// <returns></returns>
        public async Task<Event> UpdateEventAsync(Guid eventId, Event sourceEvent)
        {
            var index = Events.FindIndex(x => x.EventId == eventId);

            if (index == -1)
            {
                return sourceEvent;
            }

            sourceEvent.EventId = eventId;
            Events[index] = sourceEvent;

            return await Task.FromResult(Events[index]);
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var foundEvent = Events.Find(x => x.EventId == eventId);

            if (foundEvent == null)
            {
                return false;
            }

            var result = Events.Remove(foundEvent);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Получение списка мероприятий
        /// </summary>
        /// <returns></returns>
        public async Task<List<Event>> GetEventListAsync()
        {
            return await Task.FromResult(Events);
        }
    }
}
