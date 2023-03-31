using EventService.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SC.Internship.Common.ScResult;
using System.Net.Http.Headers;

namespace EventService.Services;

/// <summary>
/// Сервис пространств
/// </summary>
public class SpaceService : ISpaceService
{
    private readonly HttpClient _client;

    private readonly SpaceServiceConfig _config;

    private readonly ILogger<SpaceService> _logger;

    private readonly IHttpContextAccessor _contextAccessor;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    /// <param name="client"></param>
    /// <param name="contextAccessor"></param>
    public SpaceService(IOptions<SpaceServiceConfig> config,
        ILogger<SpaceService> logger,
        HttpClient client,
        IHttpContextAccessor contextAccessor)
    {
        _config = config.Value;
        _logger = logger;
        _client = client;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Проверка существования пространства
    /// </summary>
    /// <param name="spaceId"></param>
    /// <returns></returns>
    public async Task<bool> IsSpaceExists(Guid? spaceId)
    {
        var requestUri = _config.Address
                         + _config.IsSpaceExistsEndpoint?.Replace("{spaceId}", spaceId.ToString());

        _logger.LogInformation($"GET {requestUri} Parameters: {spaceId}");

        var token = _contextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

        if (AuthenticationHeaderValue.TryParse(token, out var headerValue) &&
            headerValue.Scheme == "Bearer")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", headerValue.Parameter);
        }

        var response = await _client.GetAsync(requestUri);

        var body = await response.Content.ReadAsStringAsync();
        _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

        var result = JsonConvert.DeserializeObject<ScResult<bool>>(body);

        return result.Result;
    }
}