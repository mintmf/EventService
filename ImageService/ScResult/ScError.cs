﻿namespace ImageService;

/// <summary>
/// Класс ошибки, возвращаемой из методов контроллеров
/// </summary>
public class ScError
{
    /// <summary>
    /// Интегральное сообщение об ошибке
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Сообщения об ошибках с привязкой к параметрам входной модели Acton
    /// </summary>
    public Dictionary<string, List<string>>? ModelState { get; set; }
    
}