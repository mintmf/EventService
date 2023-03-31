using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.CheckIfUserHasATicket;

/// <summary>
/// Проверить, есть ли у пользователя билет
/// </summary>
public class CheckIfUserHasATicketCommand : IRequest<ScResult<bool>>
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// ID мероприятия
    /// </summary>
    public Guid EventId { get; set; }
}