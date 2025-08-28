using Kf.Service.Warehouse.Data.Models;
using Sieve.Services;

namespace Kf.Service.Warehouse.Data.Profile;

public class WarehouseEntitySieveConfiguration : ISieveConfiguration
{
    public void Configure(
        SievePropertyMapper mapper)
    {
        mapper.Property<WarehouseEntity>(p => p.Id)
            .CanFilter();

        mapper.Property<WarehouseEntity>(p => p.Name)
            .CanFilter()
            .CanSort();

        mapper.Property<WarehouseEntity>(p => p.Location)
            .CanFilter()
            .CanSort();
    }
}
