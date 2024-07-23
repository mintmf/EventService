using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable;

/// <summary>
/// Команда проверки доступности места
/// </summary>
public class CheckIfPlaceIsAvailableCommand : IRequest<ScResult<bool>>
{
    /// <summary>
    /// Место
    /// </summary>
    public int Place { get; set; }

    /// <summary>
    /// ID мероприятия
    /// </summary>
    public Guid EventId { get; set; }
}