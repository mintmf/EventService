using FluentValidation;

namespace EventService.Features.EventFeature.CheckIfPlaceIsAvailable
{
    /// <summary>
    /// Класс для валидации проверки места
    /// </summary>
    public class CheckIfPlaceIsAvaliableValidator : AbstractValidator<Event>
    {
        /// <summary>
        /// Валидатор проверки места
        /// </summary>
        public CheckIfPlaceIsAvaliableValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.EndTime)
                .NotNull()
                .WithMessage("Такого мероприятия не существует");
        }
    }
}
