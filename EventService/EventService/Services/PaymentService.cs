using EventService.Features;
using EventService.Infrastracture;
using Microsoft.Extensions.Options;

namespace EventService.Services
{
    /// <summary>
    /// Сервис платежей
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private HttpClient _client;

        private PaymentServiceConfig _config;

        private ILogger<PaymentService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="client"></param>
        public PaymentService(IOptions<PaymentServiceConfig> config,
            ILogger<PaymentService> logger,
            HttpClient client)
        {
            _config = config.Value;
            _logger = logger;
            _client = client;
        }

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns></returns>
        public async Task<Payment> CreatePaymentAsync()
        {
            var requestUri = _config.Address + _config.CreatePayment;

            _logger.LogInformation($"POST {requestUri} Parameters: NULL");
            var response = await _client.PostAsync(requestUri, null);

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

            var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(body);

            return payment;
        }

        /// <summary>
        /// Подтвердить платеж
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<Payment> ConfirmPaymentAsync(Guid paymentId)
        {
            var requestUri = _config.Address + _config.CreatePayment;

            _logger.LogInformation($"POST {requestUri} Parameters: {paymentId}");
            var response = await _client.PutAsync(requestUri + paymentId.ToString(), null);

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

            var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(body);
            return payment;
        }

        /// <summary>
        /// Отменить платеж
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<Payment> CancelPaymentAsync(Guid paymentId)
        {
            var requestUri = _config.Address + _config.CreatePayment;

            _logger.LogInformation($"POST {requestUri} Parameters: {paymentId}");
            var response = await _client.PutAsync(requestUri + paymentId.ToString(), null);

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

            var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(body);
            return payment;
        }
    }
}
