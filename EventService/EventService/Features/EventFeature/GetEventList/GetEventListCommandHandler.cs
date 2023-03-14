using EventService.ObjectStorage;
using MediatR;
using EventService.Features.EventFeature.DeleteEvent;

namespace EventService.Features.EventFeature.GetEventList
{
    public class GetEventListCommandHandler : IRequestHandler<GetEventListCommand, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventListCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(GetEventListCommand command, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetEventListAsync();
        }
    }
}
