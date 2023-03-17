using FluentValidation.Results;

namespace EventService.Features.EventFeature.CreateEvent
{
    /// <summary>
    /// Результат создания мероприятия
    /// </summary>
    public class CreateEventResult
    {
        /// <summary>
        /// Мероприятие
        /// </summary>
        public Event? Event { get; set; }

        /// <summary>
        /// Результат валидации мероприятия
        /// </summary>
        public ValidationResult? ValidationResult { get; set; }
    }
}
