namespace Kf.Service.Warehouse.Data.Models;

public class WarehouseEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public List<InventoryEntity> Inventory { get; set; } = [];
}
