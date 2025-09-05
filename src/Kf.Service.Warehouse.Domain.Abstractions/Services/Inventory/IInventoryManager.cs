using Kf.Service.Warehouse.Domain.Models;
using Kf.Service.Warehouse.Domain.Services.Base;

namespace Kf.Service.Warehouse.Domain.Services.Inventory;

public interface IInventoryManager : IDataManager<InventoryModel>
{
    Task Sync(
        CancellationToken cancellationToken = default);
}
