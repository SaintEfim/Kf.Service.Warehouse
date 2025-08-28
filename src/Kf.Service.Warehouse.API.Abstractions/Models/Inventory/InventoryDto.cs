using System.ComponentModel.DataAnnotations;
using Kf.Service.Warehouse.API.Models.Base;

namespace Kf.Service.Warehouse.API.Models.Inventory;

public class InventoryDto : DtoBase
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Guid? WarehouseId { get; set; }
}
