using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.TicketFeature.CheckIfUserHasATicket;

/// <summary>
/// Валидация команды проверки того, что у заданного пользователя есть билет
/// </summary>
[UsedImplicitly]
public class CheckIfUserHasATicketValidator : AbstractValidator<CheckIfUserHasATicketCommand>
{
    /// <summary>
    /// Правила валидации
    /// </summary>
    public CheckIfUserHasATicketValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();

        RuleFor(c => c.UserId).NotEmpty();
    }
}