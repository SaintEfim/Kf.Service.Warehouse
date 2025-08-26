using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Abstractions.Models.Warehouse;

public class WarehouseUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}
