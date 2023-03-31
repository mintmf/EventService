using EventService.Features.EventFeature;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SC.Internship.Common.Exceptions;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// Клиент для подключения к MongoDB
    /// </summary>
    public class EventsMongoClient : MongoClient, IEventsMongoClient
    {
        private readonly EventsMongoConfig _config;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options"></param>
        public EventsMongoClient(IOptions<EventsMongoConfig> options) : base(options.Value.Address)
        {
            _config = options.Value;
        }

        /// <summary>
        /// Получить коллекцию мероприятий из MongoDB
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Event> GetEvents()
        {
            var db = GetDatabase(_config.Database);

            return db.GetCollection<Event>(_config.EventsCollection);
        }
    }
}
