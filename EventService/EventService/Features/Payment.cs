namespace EventService.Features;

/// <summary>
/// Класс платежной операции
/// </summary>
// ReSharper disable UnusedMember.Global сейчас не используется
public class Payment
{
    /// <summary>
    /// ID платежа
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Состояние платежа
    /// </summary>
    public PaymentState PaymentState { get; set; }

    /// <summary>
    /// Дата создания платежа
    /// </summary>
    public DateTimeOffset DateCreation { get; set; }
        
    /// <summary>
    /// Дата подтверждения платежа 
    /// </summary>
    public DateTimeOffset DateConfirmation { get; set; }
        
    /// <summary>
    /// Дата отмены платежа
    /// </summary>
    public DateTimeOffset DateCancellation { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
}