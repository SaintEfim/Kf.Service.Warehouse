using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Abstractions.Models.Base;

public interface IDto
{
    [Required]
    public Guid Id { get; set; }
}
