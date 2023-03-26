using EventService.Features;

namespace EventService.Infrastracture
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPaymentClient
    {
        Task<Payment> PostAsync();
    }
}
