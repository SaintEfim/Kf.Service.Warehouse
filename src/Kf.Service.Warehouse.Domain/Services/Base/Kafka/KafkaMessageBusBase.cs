using System.Text;
using Autofac.Features.Indexed;
using Confluent.Kafka;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public abstract class KafkaMessageBusBase<TMessageHandler> : IDisposable
    where TMessageHandler : IMessageHandler
{
    private volatile IProducer<string, byte[]>? _internalProducer;

    private readonly IOptions<KafkaConfig> _configKafka;

    private readonly ILogger _logger;

    private readonly IIndex<string, TMessageHandler> _handlers;

    protected KafkaMessageBusBase(
        IOptions<KafkaConfig> configKafka,
        ILogger<TMessageHandler> logger,
        IIndex<string, TMessageHandler> handlers)
    {
        _configKafka = configKafka;
        _logger = logger;
        _handlers = handlers;
    }

    private static byte[] ObjectToByteArray(
        object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var jsonString = JsonConvert.SerializeObject(obj);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    public async Task SendMessage<T>(
        T message,
        CancellationToken cancellationToken = default)
    {
        var kafkaMessage = new Message<string, byte[]>
        {
            Key = message!.GetType()
                .Name,
            Value = ObjectToByteArray(message)
        };

        var producer = GetProducer(_configKafka.Value);

        var deliveryResult = await producer.ProduceAsync(_configKafka.Value.SendConfiguration.Topic, kafkaMessage,
            cancellationToken);

        _logger.LogInformation("Message delivered to {DeliveryResultTopicPartitionOffset}",
            deliveryResult.TopicPartitionOffset);
    }

    private IProducer<string, byte[]> GetProducer(
        KafkaConfig configKafka)
    {
        if (_internalProducer != null)
        {
            return _internalProducer;
        }

        var config = new ProducerConfig
        {
            BootstrapServers = configKafka.BootstrapServers,
            MessageTimeoutMs = configKafka.SendConfiguration.MessageTimeoutMs
        };

        _internalProducer = new ProducerBuilder<string, byte[]>(config).SetLogHandler(ProducerLogHandler)
            .Build();

        return _internalProducer;
    }

    private async Task<MessageHandlingResult> HandleMessage(
        ConsumeResult<string, byte[]> result,
        CancellationToken cancellationToken)
    {
        if (result.Message == null)
        {
            return MessageHandlingResult.Dropped;
        }

        var key = result.Message.Key;
        if (string.IsNullOrEmpty(key) || !_handlers.TryGetValue(key, out var handler))
        {
            return MessageHandlingResult.Dropped;
        }

        try
        {
            return await handler.Handle(result.Message.Value, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Handler error for key {Key}", key);
            return MessageHandlingResult.FailedRequeue;
        }
    }

    public async Task StartHandling(
        CancellationToken stoppingToken = default)
    {
        if (_configKafka.Value.HandleConfiguration.Disabled || stoppingToken.IsCancellationRequested)
        {
            return;
        }

        await StartHandlingAction(stoppingToken);
    }

    private async Task StartHandlingAction(
        CancellationToken cancellationToken = default)
    {
        IConsumer<string, byte[]> consumer;
        try
        {
            consumer = GetConsumer(_configKafka);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot create consumer");
            return;
        }

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                consumer.Subscribe(_configKafka.Value.HandleConfiguration.Topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(cancellationToken);

                        var handleResult = await HandleMessage(result, cancellationToken)
                            .ConfigureAwait(false);

                        switch (handleResult)
                        {
                            case MessageHandlingResult.Dropped:
                            case MessageHandlingResult.Succeeded:
                                try
                                {
                                    consumer.Commit(result);
                                }
                                catch (Exception commitEx)
                                {
                                    _logger.LogError(commitEx, "Commit failed for topic {Topic}",
                                        _configKafka.Value.HandleConfiguration.Topic);
                                }

                                break;

                            case MessageHandlingResult.FailedRequeue:
                                break;

                            case MessageHandlingResult.FailedSentToDeadLetterQueue:
                                try
                                {
                                    consumer.Commit(result);
                                }
                                catch (Exception commitEx)
                                {
                                    _logger.LogError(commitEx, "Commit failed for topic {Topic}",
                                        _configKafka.Value.HandleConfiguration.Topic);
                                }

                                break;

                            default:
                                _logger.LogError("Unknown handle result {HandleResult}", handleResult);
                                break;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Consume error for topic {Topic}",
                            _configKafka.Value.HandleConfiguration.Topic);
                    }
                }
            }
        }
        finally
        {
            try
            {
                consumer.Close();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error while closing consumer");
            }
        }
    }

    private IConsumer<string, byte[]> GetConsumer(
        IOptions<KafkaConfig> configKafka)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = configKafka.Value.BootstrapServers,
            SessionTimeoutMs = configKafka.Value.HandleConfiguration.MessageTimeoutMs,
            EnableAutoCommit = false,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = configKafka.Value.HandleConfiguration.GroupId
        };

        return new ConsumerBuilder<string, byte[]>(config).SetLogHandler(ConsumerLogHandler)
            .Build();
    }

    private void ProducerLogHandler(
        IProducer<string, byte[]> producer,
        LogMessage logMessage)
    {
        _logger.Log((LogLevel) logMessage.LevelAs(LogLevelType.MicrosoftExtensionsLogging),
            "{KafkaInstance}|{Facility}|{Message}", logMessage.Name, logMessage.Facility, logMessage.Message);
    }

    private void ConsumerLogHandler<TKey, TValue>(
        IConsumer<TKey, TValue> consumer,
        LogMessage b)
    {
        _logger.Log((LogLevel) b.LevelAs(LogLevelType.MicrosoftExtensionsLogging),
            "{KafkaInstance}|{Facility}|{Message}", b.Name, b.Facility, b.Message);
    }

    public void Dispose()
    {
        _internalProducer?.Dispose();
    }
}
