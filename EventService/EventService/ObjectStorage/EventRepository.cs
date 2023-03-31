using EventService.Features.EventFeature;
using SC.Internship.Common.Exceptions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EventService.ObjectStorage;

/// <summary>
/// Репозиторий мероприятий
/// </summary>
public class EventRepository : IEventRepository
{
    private readonly IEventsMongoClient _mongoClient;
        
    /// <summary>
    /// Конструктор репозитория мероприятий
    /// </summary>
    /// <param name="mongoClient"></param>
    public EventRepository(IEventsMongoClient mongoClient)
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
        await _mongoClient.GetEvents().InsertOneAsync(sourceEvent);

        return sourceEvent;
    }

    /// <summary>
    /// Изменение мероприятия
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="sourceEvent"></param>
    /// <returns></returns>
    public async Task<Event> UpdateEventAsync(Guid? eventId, Event sourceEvent)
    {
        var result = await _mongoClient.GetEvents().ReplaceOneAsync(e => e.EventId == eventId, sourceEvent);

        if (result.ModifiedCount == 0)
        {
            throw new ScException("Мероприятие не найдено");
        }

        return sourceEvent;
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

        return true;
    }

    /// <summary>
    /// Получение списка мероприятий
    /// </summary>
    /// <returns></returns>
    public async Task<List<Event>> GetEventListAsync()
    {
        var events = await _mongoClient.GetEvents().Find(new BsonDocument()).ToListAsync();

        return events;
    }

    /// <summary>
    /// Удаление мероприятий
    /// </summary>
    /// <param name="spaceId"></param>
    /// <returns></returns>
    public async Task DeleteEventsBySpaceAsync(Guid spaceId)
    {
        await _mongoClient.GetEvents().DeleteManyAsync(e => e.SpaceId == spaceId);
    }

    /// <summary>
    /// Удалить изображение
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    public async Task DeleteImageAsync(Guid imageId)
    {
        var updateDefinition = Builders<Event>.Update
            .Set(e => e.PreviewImageId, Guid.Empty); 

        await _mongoClient.GetEvents()
            .UpdateManyAsync(e => e.PreviewImageId == imageId, updateDefinition);
    }

    /// <summary>
    /// Получить заданное мероприятие
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<Event> GetEventAsync(Guid eventId)
    {
        var result = await _mongoClient.GetEvents().Find(e => e.EventId == eventId).FirstOrDefaultAsync();

        if (result == null)
        {
            throw new ScException("Такого мероприятия не существует");
        }

        return result;
    }
}