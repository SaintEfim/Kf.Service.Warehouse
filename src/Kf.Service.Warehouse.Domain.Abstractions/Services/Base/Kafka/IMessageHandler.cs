using Kf.Service.Warehouse.Domain.Models.Base.Kafka;

namespace Kf.Service.Warehouse.Domain.Services.Base.Kafka;

public interface IMessageHandler
{
    Task<MessageHandlingResult> Handle(
        byte[] data,
        CancellationToken cancellationToken = default);
}

public interface IMessageHandler<in TMessage> : IMessageHandler
    where TMessage : class;
