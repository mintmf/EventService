using EventService.Features.TicketFeature;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature;

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
    public DateTimeOffset StartTime { get; set; }

    /// <summary>
    /// Конец мероприятия
    /// </summary>
    public DateTimeOffset EndTime { get; set; }

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
    // ReSharper disable once UnusedMember.Global сейчас не используется
    public decimal TicketPrice { get; set; }
}