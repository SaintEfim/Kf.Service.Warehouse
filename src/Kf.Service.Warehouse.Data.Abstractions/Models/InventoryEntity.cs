namespace Kf.Service.Warehouse.Data.Models;

public class InventoryEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid? WarehouseId { get; set; }
}
