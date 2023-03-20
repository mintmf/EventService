using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventService.ObjectStorage
{
    /// <summary>
    /// 
    /// </summary>
    public class EventsMongoClient : MongoClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public EventsMongoClient(IOptions<EventsMongoConfig> options) : base(options.Value.Address)
        {
        }
    }
}
