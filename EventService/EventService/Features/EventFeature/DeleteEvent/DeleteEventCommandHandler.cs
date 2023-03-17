using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;

namespace EventService.Features.EventFeature.DeleteEvent
{
    /// <summary>
    /// 
    /// </summary>
    [UsedImplicitly]
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventRepository"></param>
        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Обработчик команды удаления мероприятия
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(DeleteEventCommand command, CancellationToken cancellationToken)
        {
            await _eventRepository.DeleteEventAsync(command.EventId);
        }
    }
}
