﻿namespace EventService.Services;

/// <summary>
/// Интерфейс сервиса изображений
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Проверка существования изображения
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    Task<bool> IsImageExists(Guid? imageId);
}