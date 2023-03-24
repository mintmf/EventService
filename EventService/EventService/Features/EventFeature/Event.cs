using EventService.Features.TicketFeature;
using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Мероприятия
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Event
    {
        /// <summary>
        /// ID мероприятия
        /// </summary>
        public Guid? EventId { get; set; }

        /// <summary>
        /// Начало мероприятия
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Конец мероприятия
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Название мероприятия
        /// </summary>
        [UsedImplicitly]
        public string? Name { get; set; }

        /// <summary>
        /// Описания мероприятия
        /// </summary>
        [UsedImplicitly]
        public string? Description { get; set; }

        /// <summary>
        /// Картинка мероприятия
        /// </summary>
        public Guid? PreviewImageId { get; set; }

        /// <summary>
        /// Пространство, в котором проходит мероприятие
        /// </summary>
        public Guid? SpaceId { get; set; }

        /// <summary>
        /// Билеты на мероприятие
        /// </summary>
        public List<Ticket>? Tickets { get; set; }

        /// <summary>
        /// Есть ли места
        /// </summary>
        public bool PlacesAvailable { get; set; }

        /// <summary>
        /// Цена билета
        /// </summary>
        public decimal TicketPrice { get; set; }
    }
}
