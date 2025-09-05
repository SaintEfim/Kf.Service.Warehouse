using Kf.Service.Inventory.Messages.Inventory;

namespace Kf.Service.Inventory.Messages.Models;

public class InventoryListMessage
{
    public required List<InventoryData> Inventories { get; set; }
}
