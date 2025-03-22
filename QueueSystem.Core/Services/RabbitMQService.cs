using RabbitMQ.Client;
using QueueSystem.Core.Interfaces;
using System.Text;

namespace QueueSystem.Core.Services
{
    public class RabbitMQService : IQueueService
    {
        private readonly string _connectionString;
        private IConnection? _connection;
        private IModel? _channel;

        public RabbitMQService(string connectionString) => _connectionString = connectionString;

        public void Connect()
        {
            var factory = new ConnectionFactory { Uri = new Uri(_connectionString) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Enqueue(string queueName, string message)
        {
            _channel!.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }

        public string Dequeue(string queueName)
        {
            _channel!.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
            var result = _channel.BasicGet(queueName, autoAck: true);
            return result != null ? Encoding.UTF8.GetString(result.Body.ToArray()) : string.Empty;
        }

        public void Disconnect()
        {
            _channel?.Close();
            _connection?.Close();
        }

        public void Dispose() => Disconnect();
    }
}