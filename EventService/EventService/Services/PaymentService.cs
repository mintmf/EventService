using EventService.Features;
using EventService.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace EventService.Services;

/// <summary>
/// Сервис платежей
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly HttpClient _client;

    private readonly PaymentServiceConfig _config;

    private readonly ILogger<PaymentService> _logger;

    private readonly IHttpContextAccessor _contextAccessor;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    /// <param name="client"></param>
    /// <param name="contextAccessor"></param>
    public PaymentService(IOptions<PaymentServiceConfig> config,
        ILogger<PaymentService> logger,
        HttpClient client,
        IHttpContextAccessor contextAccessor)
    {
        _config = config.Value;
        _logger = logger;
        _client = client;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Создание платежа
    /// </summary>
    /// <returns></returns>
    public async Task<Payment> CreatePaymentAsync()
    {
        var requestUri = _config.Address + _config.CreatePaymentEndpoint;

        _logger.LogInformation($"POST {requestUri} Parameters: NULL");

        var token = _contextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

        if (AuthenticationHeaderValue.TryParse(token, out var headerValue) && 
            headerValue.Scheme == "Bearer")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", headerValue.Parameter);
        }

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
        var requestUri = _config.Address + _config.ConfirmPaymentEndpoint?.Replace("{paymentId}", paymentId.ToString());

        _logger.LogInformation($"POST {requestUri} Parameters: {paymentId}");

        var token = _contextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

        if (AuthenticationHeaderValue.TryParse(token, out var headerValue) &&
            headerValue.Scheme == "Bearer")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", headerValue.Parameter);
        }

        var response = await _client.PostAsync(requestUri + paymentId, null);

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
        var requestUri = _config.Address + _config.CancelPaymentEndpoint?.Replace("{paymentId}", paymentId.ToString());

        _logger.LogInformation($"POST {requestUri} Parameters: {paymentId}");

        var token = _contextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

        if (AuthenticationHeaderValue.TryParse(token, out var headerValue) &&
            headerValue.Scheme == "Bearer")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", headerValue.Parameter);
        }

        var response = await _client.PostAsync(requestUri + paymentId, null);

        var body = await response.Content.ReadAsStringAsync();
        _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

        var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<Payment>(body);
        return payment;
    }
}