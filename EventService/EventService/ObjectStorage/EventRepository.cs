using EventService.Features.EventFeature;
using EventService.Features.TicketFeature;
using Microsoft.Extensions.Logging;
using SC.Internship.Common.Exceptions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий мероприятий
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private readonly IMongoClient _mongoClient;

        private static readonly List<Event> Events = new();

        /// <summary>
        /// Конструктор репозитория мероприятий
        /// </summary>
        /// <param name="mongoClient"></param>
        public EventRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        /// <summary>
        /// Добавление мероприятия
        /// </summary>
        /// <param name="sourceEvent"></param>
        /// <returns></returns>
        public async Task<Event> AddEventAsync(Event sourceEvent)
        {
            sourceEvent.EventId = Guid.NewGuid();
            //sourceEvent.PlacesAvailable = false;

            Events.Add(sourceEvent);

            BsonDocument bson = sourceEvent.ToBsonDocument();

            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            await collection.InsertOneAsync(sourceEvent);

            return sourceEvent;
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
            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            var events = await collection.Find(new BsonDocument()).ToListAsync();

            return events;

            //return await Task.FromResult(Events);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="tickets"></param>
        /// <returns></returns>
        /// <exception cref="ScException"></exception>
        public async Task AddTicketsToAnEventAsync(Guid eventId, List<Ticket> tickets)
        {
            var foundEvent = Events.Find(x => x.EventId == eventId);

            if (foundEvent == null)
            {
                throw new ScException("Мероприятие не найдено");
            }

            if (foundEvent.Tickets == null && foundEvent.PlacesAvailable == true)
            {
                foundEvent.Tickets = new List<Ticket>();

                for (int i = 0; i < tickets.Count; i++)
                {
                    tickets[i].Place = i;
                    foundEvent.Tickets.Add(tickets[i]);
                }

                //foundEvent.Tickets = tickets;
                //foundEvent.PlacesAvailable = true;
            }

            foundEvent.Tickets = tickets;

            //foundEvent.Tickets.AddRange(tickets);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Проверить номер места на мероприятие
        /// </summary>
        /// <param name="place"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfPlaceIsAvailable(int place, Guid eventId)
        {
            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            var events = await collection.Find(new BsonDocument()).ToListAsync();

            var foundEvent = events.Find(x => x.EventId == eventId);

            if (foundEvent == null)
            {
                throw new ScException("Такого мероприятия не существует");
            }

            if(foundEvent.PlacesAvailable == false)
            {
                throw new ScException("У билетов для этого мероприятия нет мест");
            }

            if (foundEvent.Tickets == null)
            {
                throw new ScException("Для этого мероприятия нет билетов");
            }

            var foundTicket = foundEvent.Tickets.Find(p => p.Place == place);

            if (foundTicket == null)
            {
                throw new ScException("Билета с таким местом не существует");
            }

            if (foundTicket?.Owner != Guid.Empty)
            {
                return false;
            }

            return await Task.FromResult(true);
        }
    }
}
