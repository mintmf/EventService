using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.AddFreeTickets
{
    public class AddFreeTicketsCommand : IRequest<ScResult>
    {
        public AddFreeTicketsParameters Parameters { get; set; }
    }
}
