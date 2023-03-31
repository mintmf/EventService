using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature.DeleteEvent;

/// <summary>
/// Валидация удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
{
    /// <summary>
    /// Правила валидации
    /// </summary>
    public DeleteEventValidator()
    {
        RuleFor(d => d.EventId).NotEmpty();
    }
}