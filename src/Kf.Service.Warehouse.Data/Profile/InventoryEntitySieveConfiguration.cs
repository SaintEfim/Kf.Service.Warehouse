using Kf.Service.Warehouse.Data.Models;
using Sieve.Services;

namespace Kf.Service.Warehouse.Data.Profile;

public class InventoryEntitySieveConfiguration : ISieveConfiguration
{
    public void Configure(
        SievePropertyMapper mapper)
    {
        mapper.Property<InventoryEntity>(p => p.Id)
            .CanFilter();

        mapper.Property<InventoryEntity>(p => p.Name)
            .CanFilter()
            .CanSort();

        mapper.Property<InventoryEntity>(p => p.Price)
            .CanFilter()
            .CanSort();

        mapper.Property<InventoryEntity>(p => p.Quantity)
            .CanFilter()
            .CanSort();

        mapper.Property<InventoryEntity>(p => p.WarehouseId)
            .CanFilter()
            .CanSort();
    }
}
