using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Models.Base;

public class DtoBase : IDto
{
    [Required]
    public Guid Id { get; set; }
}
