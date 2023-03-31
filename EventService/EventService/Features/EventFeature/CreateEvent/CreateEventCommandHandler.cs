using EventService.ObjectStorage;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CreateEvent
{
    /// <summary>
    /// Класс обработчика команды создания нового мероприятия
    /// </summary>
    [UsedImplicitly]
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventRepository"></param>
        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ScResult<Event>> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            command.Event.EventId = Guid.NewGuid();

            var createdEvent = await _eventRepository.AddEventAsync(command.Event);

            return new ScResult<Event>
            {
                Result = createdEvent
            };
        }
    }
}
