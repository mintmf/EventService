using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.TicketFeature.GiveUserATicket;

/// <summary>
/// Валидация выдачи билета пользователю
/// </summary>
[UsedImplicitly]
public class GiveUserATicketValidator : AbstractValidator<GiveUserATicketCommand>
{
    /// <summary>
    /// Правила валидации
    /// </summary>
    public GiveUserATicketValidator()
    {
        RuleFor(g => g.TicketId).NotEmpty();

        RuleFor(g => g.Parameters).NotEmpty();
    }
}