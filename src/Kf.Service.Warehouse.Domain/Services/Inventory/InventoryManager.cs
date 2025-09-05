using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Base;
using Kf.Service.Warehouse.Messages.Warehouse;

namespace Kf.Service.Warehouse.Domain.Services.Inventory;

public class InventoryManager
    : DataManagerBase<InventoryModel, InventoryEntity, IInventoryRepository>,
        IInventoryManager
{
    private readonly IInventoryMessageBus _messageBus;

    public InventoryManager(
        IMapper mapper,
        IInventoryRepository repository,
        IInventoryMessageBus messageBus)
        : base(mapper, repository)
    {
        _messageBus = messageBus;
    }

    public async Task Sync(
        CancellationToken cancellationToken = default)
    {
        await _messageBus.SendMessage(new WarehouseInventoryListRequestMessage(), cancellationToken);
    }
}
