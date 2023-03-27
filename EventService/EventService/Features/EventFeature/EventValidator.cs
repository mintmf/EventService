using EventService.Services;
using FluentValidation;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Класс для валидации создания мероприятия
    /// </summary>
    public class EventValidator : AbstractValidator<Event>
    {
        /// <summary>
        /// Валидация
        /// </summary>
        /// <param name="imageService"></param>
        /// <param name="spaceService"></param>
        // ReSharper disable UnusedParameter.Local
        public EventValidator(IImageService imageService, ISpaceService spaceService)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.EndTime)
                .NotNull()
                .WithMessage("Время начала мероприятия не может быть null");

            RuleFor(x => x.StartTime)
                .NotNull()
                .WithMessage("Время конца мероприятия не может быть null")
                .LessThan(x => x.EndTime)
                .WithMessage("Время начала мероприятия должно быть раньше времени окончания");

            RuleFor(x => x.SpaceId)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Пространство не может быть null")
                .MustAsync(async (id, cancellation) => await spaceService.IsSpaceExists(id))
                .WithMessage("Такого пространства не существует");

            RuleFor(x => x.PreviewImageId)
                .MustAsync(async (id, cancellation) => await imageService.IsImageExists(id))
                .WithMessage("Отсутствует изображение мероприятия");
        }
    }
}
