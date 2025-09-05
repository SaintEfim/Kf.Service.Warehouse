using Microsoft.Extensions.Hosting;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public class MessageBusInitializationService : BackgroundService
{
    private readonly ITopicCreator _topicCreator;

    public MessageBusInitializationService(
        ITopicCreator topicCreator)
    {
        _topicCreator = topicCreator;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        // Если захотим создавать топики при запуске сервиса
        //return _topicCreator.CreateTopic(stoppingToken);
        return Task.CompletedTask;
    }
}
