using EventService.Services;
using FluentValidation;

namespace EventService.Features.EventFeature.CreateEvent
{
    public class CreateEventValidator : AbstractValidator<Event>
    {
        public CreateEventValidator(IImageService imageService, ISpaceService spaceService)
        {
            RuleFor(x => x.EndTime)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Время начала мероприятия не может быть null");

            RuleFor(x => x.StartTime)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Время конца мероприятия не может быть null");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithErrorCode("400")
                .WithMessage("Время начала мероприятия должно быть раньше времени окончания");

            RuleFor(x => x.SpaceId)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Пространство не может быть null");

            RuleFor(x => x.SpaceId)
                .Must(spaceService.IsSpaceExists)
                .WithErrorCode("400")
                .WithMessage("Отсутствует пространство мероприятия");

            RuleFor(x => x.PreviewImageId)
                .Must(imageService.IsImageExists)
                .WithErrorCode("400")
                .WithMessage("Отсутствует изображение мероприятия");
        }
    }
}
