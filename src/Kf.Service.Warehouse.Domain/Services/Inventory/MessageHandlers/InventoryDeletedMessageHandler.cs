using Kf.Service.Inventory.Messages.Models;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;
using Sieve.Models;

namespace Kf.Service.Warehouse.Domain.Services.Inventory.MessageHandlers;

public class InventoryDeletedMessageHandler
    : MessageHandlerBase<InventoryDeletedMessage>,
        IInventoryMessageHandler
{
    private readonly IInventoryProvider _contractorProvider;

    private readonly IInventoryManager _contractorManager;

    public InventoryDeletedMessageHandler(
        IInventoryProvider contractorProvider,
        IInventoryManager contractorManager)
    {
        _contractorProvider = contractorProvider;
        _contractorManager = contractorManager;
    }

    protected override async Task<MessageHandlingResult> Handle(
        InventoryDeletedMessage message,
        CancellationToken cancellationToken = default)
    {
        var contractor = (await _contractorProvider.Get(
                filter: new SieveModel { Filters = $"id == {message.Inventory.Id}" },
                cancellationToken: cancellationToken))
            .Any();

        if (contractor)
        {
            _ = await _contractorManager.Delete(message.Inventory.Id, cancellationToken: cancellationToken);
        }

        return MessageHandlingResult.Succeeded;
    }
}
