﻿using EventService.Services;
using FluentValidation;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature.UpdateEvent;

/// <summary>
/// Валидатор команды обновления мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public UpdateEventCommandValidator(IImageService imageService, ISpaceService spaceService)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(u => u.EventId).NotEmpty();

        RuleFor(x => x.Event.EndTime)
            .NotNull()
            .WithMessage("Время начала мероприятия не может быть null");

        RuleFor(x => x.Event.StartTime)
            .NotNull()
            .WithMessage("Время конца мероприятия не может быть null")
            .LessThan(x => x.Event.EndTime)
            .WithMessage("Время начала мероприятия должно быть раньше времени окончания");

        RuleFor(x => x.Event.SpaceId)
            .NotNull()
            .WithErrorCode("400")
            .WithMessage("Пространство не может быть null")
            .MustAsync(async (id, _) => await spaceService.IsSpaceExists(id))
            .WithMessage("Такого пространства не существует");

        RuleFor(x => x.Event.PreviewImageId)
            .NotNull()
            .MustAsync(async (id, _) => await imageService.IsImageExists(id))
            .WithMessage("Отсутствует изображение мероприятия");
    }
}