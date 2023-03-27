using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using EventService.Infrastracture;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using EventService.Features;
using EventService.ObjectStorage;

namespace EventService.Services.BackgroundServices
{
    /// <summary>
    /// Потребитель очереди
    /// </summary>
    public class RabbitMqListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly RabbitMqConfig _rabbitMqParameters;
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<RabbitMqListener> _logger;

        /// <summary>
        /// Контсруктор
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public RabbitMqListener(IOptions<RabbitMqConfig> options,
            IServiceProvider serviceProvider, ILogger<RabbitMqListener> logger)
        {
            _rabbitMqParameters = options.Value;

            _serviceProvider = serviceProvider;

            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqParameters.Address
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _rabbitMqParameters.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            // ReSharper disable once UnusedParameter.Local сейчас не используется
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                if (ProcessMessage(content))
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(_rabbitMqParameters.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

        private bool ProcessMessage(string content)
        {
            _logger.LogInformation($"Message: {content}");

            if (String.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            var rabbitMqEvent = JsonConvert.DeserializeObject<RabbitMqEvent>(content);

            if (rabbitMqEvent == null)
            {
                return false;
            }

            if (rabbitMqEvent.Type == RabbitMqEventType.EventDelete)
            {
                return false;
            }

            using IServiceScope scope = _serviceProvider.CreateScope();
            IEventRepository eventRepository =
                scope.ServiceProvider.GetRequiredService<IEventRepository>();

            if (rabbitMqEvent.Type == RabbitMqEventType.SpaceDelete)
            {
                eventRepository.DeleteEventsBySpaceAsync(rabbitMqEvent.Id);

                return true;
            }

            if (rabbitMqEvent.Type == RabbitMqEventType.ImageDelete)
            {
                eventRepository.DeleteImageAsync(rabbitMqEvent.Id);

                return true;
            }

            return false;
        }
    }
}
