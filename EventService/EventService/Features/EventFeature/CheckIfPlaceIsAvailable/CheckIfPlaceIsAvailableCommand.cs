using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable;

/// <summary>
/// 
/// </summary>
public class CheckIfPlaceIsAvailableCommand : IRequest<ScResult<bool>>
{
    /// <summary>
    /// 
    /// </summary>
    public int Place { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid EventId { get; set; }
}