using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.GiveUserATicket;

/// <summary>
/// Класс команды выдачи пользователю билета
/// </summary>
public class GiveUserATicketCommand : IRequest<ScResult<Ticket>>
{
    /// <summary>
    /// ID билета
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Параметры
    /// </summary>
    public GiveUserATicketParameters Parameters {get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parameters"></param>
    public GiveUserATicketCommand(GiveUserATicketParameters parameters)
    {
        Parameters = parameters;
    }
}