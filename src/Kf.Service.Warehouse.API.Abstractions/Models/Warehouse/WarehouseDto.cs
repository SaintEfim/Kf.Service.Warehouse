using System.ComponentModel.DataAnnotations;
using Kf.Service.Warehouse.API.Models.Base;
using Kf.Service.Warehouse.API.Models.Inventory;

namespace Kf.Service.Warehouse.API.Models.Warehouse;

public class WarehouseDto : DtoBase
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public List<InventoryDto> Inventories { get; set; } = [];
}
