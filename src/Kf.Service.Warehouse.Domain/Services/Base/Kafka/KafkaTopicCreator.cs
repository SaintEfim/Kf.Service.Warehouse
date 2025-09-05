using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public class KafkaTopicCreator
    : ITopicCreator,
        IDisposable
{
    private readonly IOptions<KafkaConfig> _configKafka;
    private readonly ILogger<KafkaTopicCreator> _logger;
    private IAdminClient? _adminClient;

    public KafkaTopicCreator(
        ILogger<KafkaTopicCreator> logger,
        IOptions<KafkaConfig> configKafka)
    {
        _logger = logger;
        _configKafka = configKafka;
    }

    public async Task CreateTopic(
        CancellationToken cancellationToken = default)
    {
        var topicSpecification = new TopicSpecification
        {
            Name = _configKafka.Value.HandleConfiguration.Topic,
            NumPartitions = 1,
            ReplicationFactor = 1
        };

        try
        {
            var adminClient = GetAdminClient();

            await adminClient.CreateTopicsAsync([topicSpecification]);
            _logger.LogInformation("Kafka topic '{TopicName}' created successfully", topicSpecification.Name);
        }
        catch (CreateTopicsException e)
        {
            if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
            {
                _logger.LogInformation("Kafka topic '{TopicName}' already exists", topicSpecification.Name);
            }
            else
            {
                _logger.LogError(e, "Failed to create Kafka topic '{TopicName}': {ErrorReason}",
                    topicSpecification.Name, e.Results[0].Error.Reason);
                throw;
            }
        }
    }

    private IAdminClient GetAdminClient()
    {
        if (_adminClient != null)
        {
            return _adminClient;
        }

        var adminConfig = new AdminClientConfig { BootstrapServers = _configKafka.Value.BootstrapServers };

        _adminClient = new AdminClientBuilder(adminConfig).Build();

        return _adminClient;
    }

    public void Dispose()
    {
        _adminClient?.Dispose();
    }
}
