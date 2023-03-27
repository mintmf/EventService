using EventService.Infrastracture;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EventService.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfig _rabbitMqParameters;

        public RabbitMqService(IOptions<RabbitMqConfig> options)
        {
            _rabbitMqParameters = options.Value;
        }

        public void SendMessage(object obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = _rabbitMqParameters.Address 
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _rabbitMqParameters.QueueName,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                   routingKey: _rabbitMqParameters.QueueName,
                                   basicProperties: null,
                                   body: body);
                }
            }
        }
    }
}
