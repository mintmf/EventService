using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventService.ObjectStorage
{
    public class EventsMongoClient : MongoClient
    {
        public EventsMongoClient(IOptions<EventsMongoConfig> options) : base(options.Value.Address)
        {
        }
    }
}
