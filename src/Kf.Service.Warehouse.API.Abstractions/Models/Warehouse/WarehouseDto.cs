using System.ComponentModel.DataAnnotations;
using Kf.Service.Warehouse.API.Abstractions.Models.Base;
using Kf.Service.Warehouse.API.Abstractions.Models.Inventory;

namespace Kf.Service.Warehouse.API.Abstractions.Models.Warehouse;

public class WarehouseDto : DtoBase
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public List<InventoryDto> Inventories { get; set; } = [];
}
