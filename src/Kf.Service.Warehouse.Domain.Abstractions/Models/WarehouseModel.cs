using Kf.Service.Warehouse.Domain.Models.Base;

namespace Kf.Service.Warehouse.Domain.Models;

public class WarehouseModel : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public List<InventoryModel> Inventories { get; set; } = [];
}
