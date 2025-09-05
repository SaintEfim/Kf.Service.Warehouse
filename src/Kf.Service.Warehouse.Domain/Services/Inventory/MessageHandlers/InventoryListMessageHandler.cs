using System.ComponentModel.DataAnnotations;
using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Kf.Service.Inventory.Messages.Inventory;
using Kf.Service.Inventory.Messages.Models;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;
using Microsoft.Extensions.Logging;

namespace Kf.Service.Warehouse.Domain.Services.Inventory.MessageHandlers;

public class InventoryListMessageHandler
    : MessageHandlerBase<InventoryListMessage>,
        IInventoryMessageHandler
{
    private readonly ILogger<InventoryListMessageHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IInventoryProvider _contractorProvider;

    private readonly IInventoryManager _contractorManager;

    private static readonly CompareLogic Comparer = CreateComparer();

    public InventoryListMessageHandler(
        IMapper mapper,
        IInventoryProvider contractorProvider,
        IInventoryManager contractorManager,
        ILogger<InventoryListMessageHandler> logger)
    {
        _mapper = mapper;
        _contractorProvider = contractorProvider;
        _contractorManager = contractorManager;
        _logger = logger;
    }

    protected override async Task<MessageHandlingResult> Handle(
        InventoryListMessage message,
        CancellationToken cancellationToken = default)
    {
        var oldInventories = await _contractorProvider.Get(cancellationToken: cancellationToken);

        await SyncInventories(oldInventories, message.Inventories, cancellationToken);

        return MessageHandlingResult.Succeeded;
    }

    private async Task SyncInventories(
        IEnumerable<InventoryModel> oldInventories,
        IEnumerable<InventoryData> newInventories,
        CancellationToken cancellationToken = default)
    {
        var oldInventoriesDictionary = oldInventories.ToDictionary(k => k.Id, v => v);

        var newInventoriesDictionary = newInventories.ToDictionary(k => k.Id, v => v);

        foreach (var newInventory in newInventoriesDictionary.Values)
        {
            try
            {
                if (oldInventoriesDictionary.TryGetValue(newInventory.Id, out var oldInventory))
                {
                    var contractorUpdate = _mapper.Map<InventoryModel>(newInventory);

                    var result = Comparer.Compare(oldInventory, contractorUpdate);

                    if (!result.AreEqual)
                    {
                        await _contractorManager.Update(contractorUpdate, cancellationToken);
                    }
                }
                else
                {
                    var contractorCreate = _mapper.Map<InventoryModel>(newInventory);

                    await _contractorManager.Create(contractorCreate, cancellationToken);
                }
            }
            catch (ValidationException exception)
            {
                _logger.LogError("{ExceptionMessage}. Id: {InventoryId}, Name: {InventoryName}", exception.Message,
                    newInventory.Id, newInventory.Name);
            }
        }

        foreach (var oldInventory in oldInventoriesDictionary.Values.Where(oldInventory =>
                     !newInventoriesDictionary.ContainsKey(oldInventory.Id)))
        {
            await _contractorManager.Delete(oldInventory.Id, cancellationToken);
        }
    }

    private static CompareLogic CreateComparer()
    {
        return new CompareLogic
        {
            Config = new ComparisonConfig
            {
                CompareChildren = true,
                IgnoreCollectionOrder = true,
                IgnoreObjectTypes = true
            }
        };
    }
}
