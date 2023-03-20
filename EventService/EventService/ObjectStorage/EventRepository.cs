using EventService.Features.EventFeature;
using EventService.Features.TicketFeature;
using Microsoft.Extensions.Logging;
using SC.Internship.Common.Exceptions;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Runtime.CompilerServices;

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

            Events.Add(sourceEvent);

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
            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            var updateFilter = Builders<Event>.Filter.Eq("EventId", eventId);

            var result = await collection.ReplaceOneAsync(updateFilter, sourceEvent);

            if (result.ModifiedCount == 0)
            {
                throw new ScException("Мероприятие не найдено");
            }

            return await Task.FromResult(sourceEvent);
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            var deleteFilter = Builders<Event>.Filter.Eq("EventId", eventId);

            var result = await collection.DeleteOneAsync(deleteFilter);

            if (result.DeletedCount == 0)
            {
                throw new ScException("Мероприятие не найдено");
            }

            return await Task.FromResult(true);
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
        }

        /// <summary>
        /// Добавить бесплатные билеты
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="tickets"></param>
        /// <returns></returns>
        /// <exception cref="ScException"></exception>
        public async Task AddTicketsToAnEventAsync(Guid eventId, List<Ticket> tickets)
        {
            var db = _mongoClient.GetDatabase("event_database");
            var collection = db.GetCollection<Event>("events");
            var eventFilter = Builders<Event>.Filter.Eq("EventId", eventId);

            var result = await collection.Find(eventFilter).Limit(1).SingleAsync();
            
            if (result == null)
            {
                throw new ScException("Мероприятие не найдено");
            }

            var foundEvent = result;
            if (foundEvent.Tickets == null)
            {
                foundEvent.Tickets = new List<Ticket>();

                for (int i = 0; i < tickets.Count; i++)
                {
                    if (foundEvent.PlacesAvailable == true)
                    {
                        tickets[i].Place = i + 1;
                    }
                    foundEvent.Tickets.Add(tickets[i]);
                }

                await collection.ReplaceOneAsync(eventFilter, foundEvent);
            }

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

            if (foundEvent.PlacesAvailable == false)
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
