using EventService.ObjectStorage;
using MediatR;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature.GetEventList
{
    /// <summary>
    /// Класс обработчика команды получения списка всех мероприятий
    /// </summary>
    [UsedImplicitly]
    public class GetEventListCommandHandler : IRequestHandler<GetEventListCommand, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventRepository"></param>
        public GetEventListCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Обработчик команды получения списка вмех мероприятий
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Event>> Handle(GetEventListCommand command, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetEventListAsync();
        }
    }
}
