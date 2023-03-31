using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SC.Internship.Common.Exceptions;

namespace EventService.Features.EventFeature.UpdateEvent
{
    /// <summary>
    /// Класс обработчика команды изменения мероприятия
    /// </summary>
    [UsedImplicitly]
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventRepository"></param>
        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
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
            command.Event.EventId = command.EventId;

            var result = await _eventRepository.UpdateEventAsync(command.EventId, command.Event);

            return await Task.FromResult(result);
        }
    }
}
