namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public interface IMessageBus
{
    Task SendMessage<T>(
        T message,
        CancellationToken cancellationToken = default);

    Task StartHandling(
        CancellationToken stoppingToken = default);
}
