using Kf.Service.Warehouse.Domain.Models.Base;

namespace Kf.Service.Warehouse.Domain.Models;

public class InventoryModel : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid? WarehouseId { get; set; }
}
