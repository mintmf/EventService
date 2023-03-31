using FluentValidation;

namespace EventService.Features.EventFeature.DeleteEvent
{
    /// <summary>
    /// Валидация удаления мерприятия
    /// </summary>
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
}
