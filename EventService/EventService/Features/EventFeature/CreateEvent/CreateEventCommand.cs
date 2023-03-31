using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CreateEvent;

/// <summary>
/// 
/// </summary>
public class CreateEventCommand : IRequest<ScResult<Event>>
{
    /// <summary>
    /// 
    /// </summary>
    public Event Event { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CreateEventCommand()
    {
        Event = new Event();
    }
}