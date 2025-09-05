using System.Text;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;

public abstract class MessageHandlerBase<TMessage> : IMessageHandler<TMessage>
    where TMessage : class
{
    private static TMessage? Deserialize(
        byte[] data)
    {
        using var stream = new MemoryStream(data, false);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        return (TMessage?) JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            })
            .Deserialize(reader, typeof(TMessage));
    }

    public virtual Task<MessageHandlingResult> Handle(
        byte[] data,
        CancellationToken cancellationToken = default)
    {
        var message = Deserialize(data);

        return message == null
            ? Task.FromResult(MessageHandlingResult.FailedSentToDeadLetterQueue)
            : Handle(message, cancellationToken);
    }

    protected abstract Task<MessageHandlingResult> Handle(
        TMessage message,
        CancellationToken cancellationToken = default);
}
