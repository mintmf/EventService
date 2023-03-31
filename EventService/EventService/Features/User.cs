using System.Diagnostics.CodeAnalysis;

namespace EventService.Features;

/// <summary>
/// Класс пользователя
/// </summary>
[SuppressMessage("ReSharper", "All")] // Пока класс не используется
public class User
{
    /// <summary>
    /// ID пользователя
    /// </summary>

    public Guid UserId { get; set; }

    /// <summary>
    /// Ник пользователя
    /// </summary>
    public string? Nickname { get; set; }
}