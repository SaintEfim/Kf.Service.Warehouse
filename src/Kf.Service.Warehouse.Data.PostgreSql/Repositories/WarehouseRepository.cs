using Kf.Service.Warehouse.Data.PostgreSql.Context;
using Kf.Service.Warehouse.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace Kf.Service.Warehouse.Data.PostgreSql.Repositories;

public class WarehouseRepository : WarehouseRepository<DbContext>
{
    public WarehouseRepository(
        WarehouseDbContext dbContext,
        ISieveProcessor sieveProcessor)
        : base(dbContext, sieveProcessor)
    {
    }
}
