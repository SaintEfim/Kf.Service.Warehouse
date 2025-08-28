using System.ComponentModel.DataAnnotations;

namespace Kf.Service.Warehouse.API.Models.Base;

public interface IDto
{
    public Guid Id { get; set; }
}
