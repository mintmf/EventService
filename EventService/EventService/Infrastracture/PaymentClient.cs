using EventService.Features;
using Microsoft.Extensions.Options;

namespace EventService.Infrastracture
{
    public class PaymentClient : IPaymentClient
    {
        private HttpClient _httpClient;
        private PaymentServiceConfig _config;

        public PaymentClient(HttpClient httpClient, IOptions<PaymentServiceConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Payment> PostAsync()
        {
            var requestUri = _config.Address + _config.CreatePayment;

            var response = await _httpClient.PostAsync(requestUri, null);

            var body = await response.Content.ReadAsStringAsync();

            var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(body);
            return payment;
        }
    }
}
