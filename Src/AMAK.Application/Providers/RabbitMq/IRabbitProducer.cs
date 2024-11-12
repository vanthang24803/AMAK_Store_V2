namespace AMAK.Application.Providers.RabbitMq {
    public interface IRabbitProducer {
        void SendMessage<T>(string queue, T message);
    }
}