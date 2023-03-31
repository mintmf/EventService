namespace EventService.Features.TicketFeature.AddFreeTickets;

/// <summary>
/// Параметры метода, который создает бесплатные билеты на мероприятие
/// </summary>
public class AddFreeTicketsParameters
{
    /// <summary>
    /// Количество билетов
    /// </summary>
    public uint NumberOfTickets { get; set; }

    /// <summary>
    /// ID мероприятия
    /// </summary>
    public Guid EventId { get; set; }
}