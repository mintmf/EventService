using EventService.ObjectStorage;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Features.EventFeature.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreateEventResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<Event> _validator;

        public CreateEventCommandHandler(IEventRepository eventRepository, IValidator<Event> validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }
        public async Task<CreateEventResult> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command.Event);

            if (!validationResult.IsValid)
            {
                return new CreateEventResult
                {
                    Event = null,
                    ValidationResult = validationResult
                };
            }
                
            var createdEvent = await _eventRepository.AddEventAsync(command.Event);

            return new CreateEventResult
            {
                Event = createdEvent,
                ValidationResult = validationResult
            };
        }
    }
}
