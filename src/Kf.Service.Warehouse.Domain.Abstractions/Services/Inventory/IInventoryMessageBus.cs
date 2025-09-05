using Kf.Service.Warehouse.Domain.Services.Base.Kafka;

namespace Kf.Service.Warehouse.Domain.Services.Inventory;

public interface IInventoryMessageBus
    : IMessageBus,
        IDisposable;
