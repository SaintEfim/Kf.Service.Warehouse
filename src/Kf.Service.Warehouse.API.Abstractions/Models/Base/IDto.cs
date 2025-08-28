using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Models.Base;

public interface IDto
{
    [Required]
    public Guid Id { get; set; }
}
