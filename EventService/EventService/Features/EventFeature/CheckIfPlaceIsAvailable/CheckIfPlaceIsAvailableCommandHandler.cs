using EventService.ObjectStorage;
using FluentValidation;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckIfPlaceIsAvailableCommandHandler : IRequestHandler<CheckIfPlaceIsAvailableCommand, ScResult<bool>>
    {
        IEventRepository _eventRepository;
        private readonly IValidator<Event> _validator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventRepository"></param>
        /// <param name="validator"></param>
        public CheckIfPlaceIsAvailableCommandHandler(IEventRepository eventRepository, IValidator<Event> validator)
        {
            _eventRepository = eventRepository;
            _validator = validator; 
        }

        /// <summary>
        /// 
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
