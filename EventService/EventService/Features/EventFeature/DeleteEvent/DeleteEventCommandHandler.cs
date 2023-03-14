using EventService.ObjectStorage;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventService.Features.EventFeature.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(DeleteEventCommand command, CancellationToken cancellationToken)
        {
            await _eventRepository.DeleteEventAsync(command.EventId);
        }
    }
}
