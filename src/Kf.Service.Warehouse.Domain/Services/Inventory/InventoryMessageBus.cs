using Autofac.Features.Indexed;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kf.Service.Warehouse.Domain.Services.Inventory;

public class InventoryMessageBus
    : KafkaMessageBusBase<IInventoryMessageHandler>,
        IInventoryMessageBus
{
    public InventoryMessageBus(
        IOptions<KafkaConfig> configKafka,
        ILogger<IInventoryMessageHandler> logger,
        IIndex<string, IInventoryMessageHandler> handlers)
        : base(configKafka, logger, handlers)
    {
    }
}
