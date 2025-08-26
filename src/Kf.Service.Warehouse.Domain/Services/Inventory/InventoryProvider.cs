using AutoMapper;
using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories;
using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Base;

namespace Kf.Service.Warehouse.Domain.Services.Inventory;

public class InventoryProvider : DataProviderBase<InventoryModel, InventoryEntity, IInventoryRepository>
{
    public InventoryProvider(
        IMapper mapper,
        IInventoryRepository repository)
        : base(mapper, repository)
    {
    }
}
