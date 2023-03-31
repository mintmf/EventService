using EventService.Features.EventFeature;
using MongoDB.Driver;

namespace EventService.ObjectStorage;

/// <summary>
/// Интерфейс клиента для подключения к MongoDB
/// </summary>
public interface IEventsMongoClient : IMongoClient
{
    /// <summary>
    /// Получить коллекцию мероприятий из MongoDB
    /// </summary>
    /// <returns></returns>
    IMongoCollection<Event> GetEvents();
}