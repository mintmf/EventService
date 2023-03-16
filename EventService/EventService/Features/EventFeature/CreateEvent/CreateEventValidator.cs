using EventService.Services;
using FluentValidation;

namespace EventService.Features.EventFeature.CreateEvent
{
    public class CreateEventValidator : AbstractValidator<Event>
    {
        public CreateEventValidator(IImageService imageService, ISpaceService spaceService)
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
            /*
                        RuleFor(x => x.StartTime)
                            .LessThan(x => x.EndTime)
                            .WithMessage("Время начала мероприятия должно быть раньше времени окончания");*/

            RuleFor(x => x.SpaceId)
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Пространство не может быть null")
                .Must(spaceService.IsSpaceExists)
                .WithMessage("Время начала мероприятия должно быть раньше времени окончания");

            /*RuleFor(x => x.SpaceId)
                .Must(spaceService.IsSpaceExists)
                .WithMessage("Отсутствует пространство мероприятия");*/

            RuleFor(x => x.PreviewImageId)
                .Must(imageService.IsImageExists)
                .WithMessage("Отсутствует изображение мероприятия");
        }
    }
}
