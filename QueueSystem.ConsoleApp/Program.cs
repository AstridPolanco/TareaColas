using Microsoft.Extensions.Configuration;
using QueueSystem.Core.Interfaces;
using QueueSystem.Core.Services;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var queueProvider = config["QueueProvider"] ?? throw new InvalidOperationException("QueueProvider no configurado.");
var connectionString = config.GetConnectionString(queueProvider) ?? throw new InvalidOperationException($"Cadena de conexión para {queueProvider} no encontrada.");


IQueueService queueService = config["QueueProvider"] switch
{
    "ActiveMQ" => new ActiveMQService(config.GetConnectionString("ActiveMQ")),
    "RabbitMQ" => new RabbitMQService(config.GetConnectionString("RabbitMQ")),
    "AzureQueueStorage" => new AzureQueueStorageService(config.GetConnectionString("AzureQueueStorage")),
    _ => throw new NotSupportedException("Queue service not supported")
};

queueService.Connect();

//Ejemplo de uso
queueService.Enqueue("queue1", "Hello World!");
var message = queueService.Dequeue("queue1");
Console.WriteLine($"Mensaje recibido: {message}");

queueService.Disconnect();
