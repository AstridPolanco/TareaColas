using Apache.NMS;
using Apache.NMS.ActiveMQ;
using QueueSystem.Core.Interfaces;

namespace QueueSystem.Core.Services
{
    public class ActiveMQService : IQueueService
    {
        private readonly string _connectionString;
        private IConnection? _connection;
        private ISession? _session;

        public ActiveMQService(string connectionString) => _connectionString = connectionString;

        public void Connect()
        {
            var factory = new ConnectionFactory(_connectionString);
            _connection = factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();
        }

        public void Enqueue(string queueName, string message)
        {
            using var destination = _session!.GetQueue(queueName);
            using var producer = _session!.CreateProducer(destination);
            producer.Send(_session!.CreateTextMessage(message));
        }

        public string Dequeue(string queueName)
        {
            using var destination = _session!.GetQueue(queueName);
            using var consumer = _session!.CreateConsumer(destination);
            var message = consumer.Receive() as ITextMessage;
            return message?.Text ?? string.Empty;
        }

        public void Disconnect()
        {
            _session?.Close();
            _connection?.Close();
        }

        public void Dispose() => Disconnect();
    }
}