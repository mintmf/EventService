namespace EventService.Features;

/// <summary>
/// Состояние платежа
/// </summary>
// ReSharper disable UnusedMember.Global сейчас не используется
public enum PaymentState
{
    /// <summary>
    /// Ожидание выполнения операции передачи билета пользователю
    /// </summary>
    Hold,

    /// <summary>
    /// Подтверждение платежа
    /// </summary>
    Confirmed,

    /// <summary>
    /// Отмена платежа
    /// </summary>
    Canceled
}