using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace Kf.Service.Warehouse.Data.Repositories;

public class WarehouseRepository<TDbContext>
    : RepositoryBase<TDbContext, WarehouseEntity>,
        IWarehouseRepository
    where TDbContext : DbContext
{
    protected WarehouseRepository(
        TDbContext dbContext,
        ISieveProcessor sieveProcessor)
        : base(dbContext, sieveProcessor)
    {
    }

    protected override IQueryable<WarehouseEntity> FillRelatedRecords(
        IQueryable<WarehouseEntity> query)
    {
        return query.Include(x => x.Inventories);
    }
}
