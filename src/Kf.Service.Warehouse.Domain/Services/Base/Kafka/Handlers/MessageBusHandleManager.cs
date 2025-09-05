using Microsoft.Extensions.Hosting;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;

public sealed class MessageBusHandleManager : BackgroundService
{
    private readonly IEnumerable<IMessageBus> _messageBuses;

    public MessageBusHandleManager(
        IEnumerable<IMessageBus> messageBuses)
    {
        _messageBuses = messageBuses;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        return Parallel.ForEachAsync(_messageBuses, stoppingToken, (
            messageBus,
            cancellation) => new ValueTask(messageBus.StartHandling(cancellation)));
    }
}
