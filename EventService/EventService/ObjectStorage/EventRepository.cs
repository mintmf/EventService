using EventService.Features.EventFeature;
using EventService.Features.TicketFeature;
using SC.Internship.Common.Exceptions;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Репозиторий мероприятий
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private readonly IEventsMongoClient _mongoClient;
        private readonly EventsMongoConfig _config;
        
        /// <summary>
        /// Конструктор репозитория мероприятий
        /// </summary>
        /// <param name="mongoClient"></param>
        /// <param name="options"></param>
        public EventRepository(IEventsMongoClient mongoClient, IOptions<EventsMongoConfig> options)
        {
            _mongoClient = mongoClient;
            _config = options.Value;
        }

        /// <summary>
        /// Добавление мероприятия
        /// </summary>
        /// <param name="sourceEvent"></param>
        /// <returns></returns>
        public async Task<Event> AddEventAsync(Event sourceEvent)
        {
            sourceEvent.EventId = Guid.NewGuid();

            var db = _mongoClient.GetDatabase(_config.Database);
            var collection = db.GetCollection<Event>(_config.EventsCollection);

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
            var result = await _mongoClient.GetEvents().ReplaceOneAsync(e => e.EventId == eventId, sourceEvent);

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
            var result = await _mongoClient.GetEvents().DeleteOneAsync(e => e.EventId == eventId);

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
            //var db = _mongoClient.GetDatabase(_config.Database);
            //var collection = db.GetCollection<Event>(_config.EventsCollection);
            var events = await _mongoClient.GetEvents().Find(new BsonDocument()).ToListAsync();

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
        // FindEvent ( ... )
        {
            var db = _mongoClient.GetDatabase(_config.Database);
            var collection = db.GetCollection<Event>(_config.EventsCollection);
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
                    if (foundEvent.PlacesAvailable)
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
        // GetEvent( ... )
        {
            var db = _mongoClient.GetDatabase(_config.Database);
            var collection = db.GetCollection<Event>(_config.EventsCollection);
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

            if (foundTicket.Owner != Guid.Empty)
            {
                return false;
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Удаление мероприятий
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public async Task DeleteEventsBySpaceAsync(Guid spaceId)
        {
            var db = _mongoClient.GetDatabase(_config.Database);
            var collection = db.GetCollection<Event>(_config.EventsCollection);
            var deleteFilter = Builders<Event>.Filter.Eq("SpaceId", spaceId);

            await collection.DeleteManyAsync(deleteFilter);
        }

        /// <summary>
        /// Удалить изображение
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task DeleteImageAsync(Guid imageId)
        {
            var db = _mongoClient.GetDatabase(_config.Database);
            var collection = db.GetCollection<Event>(_config.EventsCollection);
            var updateFilter = Builders<Event>.Filter.Eq("PreviewImageId", imageId);

            var updateDefinition = Builders<Event>.Update
                    .Set(e => e.PreviewImageId, Guid.Empty); 

            await collection.UpdateManyAsync(updateFilter, updateDefinition);
        }
    }
}
