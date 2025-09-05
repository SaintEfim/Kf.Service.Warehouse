namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public interface ITopicCreator
{
    Task CreateTopic(
        CancellationToken cancellationToken = default);
}
