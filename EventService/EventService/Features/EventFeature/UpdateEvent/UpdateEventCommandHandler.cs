using EventService.ObjectStorage;
using MediatR;

namespace EventService.Features.EventFeature.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            return await _eventRepository.UpdateEventAsync(command.EventId, command.Event);
        }
    }
}
