using FluentValidation.Results;
using MediatR;

namespace EventService.Features.EventFeature.CreateEvent
{
    public class CreateEventCommand : IRequest<CreateEventResult>
    {
        public Event Event { get; set; }

        public CreateEventCommand()
        {
            Event = new Event();
        }
    }
}
