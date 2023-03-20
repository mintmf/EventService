using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable
{
    /// <summary>
    /// Обработчик команды проверки места
    /// </summary>
    [UsedImplicitly]
    public class CheckIfPlaceIsAvailableCommandHandler : IRequestHandler<CheckIfPlaceIsAvailableCommand, ScResult<bool>>
    {
        private readonly IEventRepository _eventRepository;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventRepository"></param>
        public CheckIfPlaceIsAvailableCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Обработчик
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ScResult<bool>> Handle(CheckIfPlaceIsAvailableCommand command, CancellationToken cancellationToken)
        {
            var result = await _eventRepository.CheckIfPlaceIsAvailable(command.Place, command.EventId);

            return new ScResult<bool>
            {
                Result = result
            };
        }
    }
}
