using EventService.ObjectStorage;
using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.TicketFeature.AddFreeTickets;

/// <summary>
/// Валидатор добавления билетов к мероприятию
/// </summary>
[UsedImplicitly]
public class AddFreeTicketsValidator : AbstractValidator<AddFreeTicketsCommand>
{
    /// <summary>
    /// Правила валидации
    /// </summary>
    /// <param name="client"></param>
    public AddFreeTicketsValidator(IEventRepository client)
    {
        RuleFor(e => client.GetEventAsync(e.Parameters.EventId).Result.Tickets).Null();
    }
}