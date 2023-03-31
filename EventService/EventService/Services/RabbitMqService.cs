using EventService.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EventService.Services;

/// <summary>
/// Класс сервиса RabbitMQ
/// </summary>
public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfig _rabbitMqParameters;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options"></param>
    public RabbitMqService(IOptions<RabbitMqConfig> options)
    {
        _rabbitMqParameters = options.Value;
    }

    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="obj"></param>
    public void SendMessage(object obj)
    {
        var message = JsonConvert.SerializeObject(obj);
        SendMessage(message);
    }

    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory 
        { 
            HostName = _rabbitMqParameters.Address 
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
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