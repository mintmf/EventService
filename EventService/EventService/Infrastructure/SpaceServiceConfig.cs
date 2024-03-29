﻿namespace EventService.Infrastructure;

/// <summary>
/// Конфигурация сервиса изображений
/// </summary>
public class SpaceServiceConfig
{
    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Адрес метода проверки существования пространства
    /// </summary>
    public string? IsSpaceExistsEndpoint { get; set; }
}