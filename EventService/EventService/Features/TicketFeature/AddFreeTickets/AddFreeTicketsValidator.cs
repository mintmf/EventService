using EventService.ObjectStorage;
using FluentValidation;

namespace EventService.Features.TicketFeature.AddFreeTickets
{
    /// <summary>
    /// Валидатор добавления билетов к мероприятию
    /// </summary>
    public class AddFreeTicketsValidator : AbstractValidator<AddFreeTicketsCommand>
    {
        private readonly IEventRepository _client;

        /// <summary>
        /// Правила валидации
        /// </summary>
        /// <param name="client"></param>
        public AddFreeTicketsValidator(IEventRepository client)
        {
            _client = client;

            RuleFor(e => _client.GetEventAsync(e.Parameters.EventId).Result.Tickets).Null();
        }
    }
}
