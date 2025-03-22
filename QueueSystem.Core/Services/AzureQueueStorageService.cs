using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using QueueSystem.Core.Interfaces;

namespace QueueSystem.Core.Services
{
    public class AzureQueueStorageService : IQueueService, IDisposable
    {
        private readonly string _connectionString;
        private QueueClient? _queueClient;

        public AzureQueueStorageService(string connectionString) => _connectionString = connectionString;

        public void Connect(){}
        public void Enqueue(string queueName, string message)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            _queueClient.CreateIfNotExists();
            _queueClient.SendMessage(message);
        }

        public string Dequeue(string queueName)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            QueueMessage[] messages = _queueClient.ReceiveMessages();
            return messages.Length > 0 ? messages[0].Body.ToString() : string.Empty;
        }

        public void Disconnect(){}

        public void Dispose() => _queueClient?.DeleteIfExists();
    }
}