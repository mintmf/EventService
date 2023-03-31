using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable;

/// <summary>
/// Валидатор проверки места
/// </summary>
[UsedImplicitly]
public class CheckIfPlaceIsAvailableValidator : AbstractValidator<CheckIfPlaceIsAvailableCommand>
{
    /// <summary>
    /// Правила валидации
    /// </summary>
    public CheckIfPlaceIsAvailableValidator()
    {
        RuleFor(c => c.EventId).NotNull();
    }
}