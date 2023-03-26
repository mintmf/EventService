using EventService.Features;
using EventService.Infrastracture;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SC.Internship.Common.ScResult;

namespace EventService.Services
{
    /// <summary>
    /// Сервис изображений
    /// </summary>
    public class ImageService : IImageService
    {
        private readonly HttpClient _client;
        private readonly ImageServiceConfig _config;
        private readonly ILogger<ImageService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="client"></param>
        public ImageService(IOptions<ImageServiceConfig> config, 
            ILogger<ImageService> logger,
            HttpClient client)
        {
            _config = config.Value;
            _logger = logger;
            _client = client;
        }

        /// <summary>
        /// Проверка существования изображения
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task<bool> IsImageExists(Guid? imageId)
        {
            var requestUri = _config.Address
                + _config.IsImageExistsEndpoint.Replace("{imageId}", imageId.ToString());

            _logger.LogInformation($"GET {requestUri} Parameters: {imageId}");
            var response = await _client.GetAsync(requestUri);

            var body = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Status: {response.StatusCode} Response: {body}");

            var result = JsonConvert.DeserializeObject<ScResult<bool>>(body);

            return result.Result;
        }
    }
}
