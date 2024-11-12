using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AMAK.Application.Providers.RabbitMq {
    public class RabbitProducer : IRabbitProducer {
        public readonly string RabbitHost;
        public RabbitProducer(Microsoft.Extensions.Configuration.IConfiguration configuration) {
            RabbitHost = configuration["RabbitConfig:HostName"]!;
        }
        public void SendMessage<T>(string queue, T message) {
            var factory = new ConnectionFactory() {
                HostName = RabbitHost,
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);

            var jsonString = JsonConvert.SerializeObject(message, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", queue, body: body);
        }
    }
}