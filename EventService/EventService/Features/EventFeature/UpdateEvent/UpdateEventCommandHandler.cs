using EventService.ObjectStorage;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace EventService.Features.EventFeature.UpdateEvent
{
    /// <summary>
    /// Класс обработчика команды изменения мероприятия
    /// </summary>
    [UsedImplicitly]
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<Event> _validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventRepository"></param>
        /// <param name="validator"></param>
        public UpdateEventCommandHandler(IEventRepository eventRepository, IValidator<Event> validator)
        {
            _validator = validator;
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Обработчик команды изменения мероприятия
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command.Event, cancellationToken);

            return await _eventRepository.UpdateEventAsync(command.EventId, command.Event);
        }
    }
}
