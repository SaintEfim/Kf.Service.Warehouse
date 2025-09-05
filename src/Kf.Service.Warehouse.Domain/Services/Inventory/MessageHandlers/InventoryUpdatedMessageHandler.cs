using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Kf.Service.Inventory.Messages.Models;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Models.Base.Kafka;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;
using Microsoft.Extensions.Logging;
using Sieve.Models;

namespace Kf.Service.Warehouse.Domain.Services.Inventory.MessageHandlers;

public class InventoryUpdatedMessageHandler
    : MessageHandlerBase<InventoryUpdatedMessage>,
        IInventoryMessageHandler
{
    private readonly ILogger<InventoryUpdatedMessageHandler> _logger;

    private readonly IInventoryProvider _contractorProvider;

    private readonly IInventoryManager _contractorManager;

    private readonly IMapper _mapper;

    public InventoryUpdatedMessageHandler(
        IMapper mapper,
        IInventoryProvider contractorProvider,
        IInventoryManager contractorManager,
        ILogger<InventoryUpdatedMessageHandler> logger)
    {
        _mapper = mapper;
        _contractorProvider = contractorProvider;
        _contractorManager = contractorManager;
        _logger = logger;
    }

    protected override async Task<MessageHandlingResult> Handle(
        InventoryUpdatedMessage message,
        CancellationToken cancellationToken = default)
    {
        var contractor = (await _contractorProvider.Get(
                filter: new SieveModel { Filters = $"id == {message.Inventory.Id}" },
                cancellationToken: cancellationToken))
            .SingleOrDefault();

        try
        {
            if (contractor == null)
            {
                contractor = _mapper.Map<InventoryModel>(message.Inventory);
                _ = await _contractorManager.Create(contractor, cancellationToken: cancellationToken);
            }
            else
            {
                _mapper.Map(message.Inventory, contractor);
                _ = await _contractorManager.Update(contractor, cancellationToken: cancellationToken);
            }
        }
        catch (ValidationException exception)
        {
            _logger.LogError("{ExceptionMessage}. Id: {InventoryId}, Name: {InventoryName}", exception.Message,
                message.Inventory.Id, message.Inventory.Name);
        }

        return MessageHandlingResult.Succeeded;
    }
}
