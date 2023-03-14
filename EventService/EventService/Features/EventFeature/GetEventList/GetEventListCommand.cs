using MediatR;

namespace EventService.Features.EventFeature.GetEventList
{
    public class GetEventListCommand : IRequest<List<Event>>
    {
    }
}
