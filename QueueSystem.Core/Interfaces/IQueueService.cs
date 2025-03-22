

namespace QueueSystem.Core.Interfaces
{
    public interface IQueueService
    {
        void Enqueue(string queueName, string message);
        string Dequeue(string queueName);
        void Connect();
        void Disconnect();
    }
}