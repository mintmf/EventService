using EventService.Infrastracture;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SC.Internship.Common.ScResult;
using static System.Net.Mime.MediaTypeNames;

namespace EventService.Services
{
    /// <summary>
    /// Сервис пространств
    /// </summary>
    public class SpaceService : ISpaceService
    {
        static readonly HttpClient client = new();
        private readonly SpaceServiceConfig _config;
        private readonly ILogger<SpaceService> _logger;

        /// <summary>
        /// Конструткор
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public SpaceService(IOptions<SpaceServiceConfig> config, ILogger<SpaceService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        /// <summary>
        /// Проверка существования пространства
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public async Task<bool> IsSpaceExists(Guid? spaceId)
        {
            var requestUri = _config.Address
                + _config.IsSpaceExistsEndpoint.Replace("{spaceId}", spaceId.ToString());

            _logger.LogInformation($"GET {requestUri} Parameters: {spaceId}");
            var response = await client.GetAsync(requestUri);

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

            var result = JsonConvert.DeserializeObject<ScResult<bool>>(body);

            return result.Result;
        }
    }
}
