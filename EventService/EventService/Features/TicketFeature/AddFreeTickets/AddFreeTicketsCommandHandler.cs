using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.AddFreeTickets
{
    public class AddFreeTicketsCommandHandler : IRequestHandler<AddFreeTicketsCommand, ScResult>
    {
        public Task<ScResult> Handle(AddFreeTicketsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
