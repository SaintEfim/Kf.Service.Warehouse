using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Abstractions.Models.Inventory;

public class InventoryUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Guid? WarehouseId { get; set; }
}
