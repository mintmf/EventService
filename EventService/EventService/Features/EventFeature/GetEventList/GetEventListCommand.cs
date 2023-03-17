using MediatR;

namespace EventService.Features.EventFeature.GetEventList
{
    /// <summary>
    /// 
    /// </summary>
    public class GetEventListCommand : IRequest<List<Event>>
    {
    }
}
