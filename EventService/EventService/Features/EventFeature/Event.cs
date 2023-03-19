using EventService.Features.TicketFeature;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Мероприятия
    /// </summary>
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
        /// Максимальное число билетов
        /// </summary>
        public int MaxNumberOfTickets { get; set; }
        /// <summary>
        /// Проверить, есть ли доступные билеты
        /// </summary>
        public bool PlacesAvailable { get; set; }
    }
}
