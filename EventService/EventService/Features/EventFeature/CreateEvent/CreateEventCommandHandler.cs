using EventService.ObjectStorage;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<Event> _validator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventRepository"></param>
        /// <param name="validator"></param>
        public CreateEventCommandHandler(IEventRepository eventRepository, IValidator<Event> validator)
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
        public async Task<ScResult<Event>> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command.Event, cancellationToken);
                
            var createdEvent = await _eventRepository.AddEventAsync(command.Event);

            return new ScResult<Event>
            {
                Result = createdEvent
            };
        }
    }
}
