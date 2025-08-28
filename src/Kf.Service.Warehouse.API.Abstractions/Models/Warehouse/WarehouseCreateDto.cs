using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Models.Warehouse;

public class WarehouseCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}
