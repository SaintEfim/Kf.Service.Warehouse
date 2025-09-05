using Kf.Service.Inventory.Messages.Inventory;

namespace Kf.Service.Inventory.Messages.Models;

public class InventoryChangedMessage
{
    public required InventoryData Inventory { get; set; }
}
