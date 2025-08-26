using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.Repositories;

public class WarehouseRepositoryBase<TDbContext> : RepositoryBase<TDbContext, WarehouseEntity>
    where TDbContext : DbContext
{
    protected WarehouseRepositoryBase(
        TDbContext dbContext)
        : base(dbContext)
    {
    }

    protected override IQueryable<WarehouseEntity> FillRelatedRecords(
        IQueryable<WarehouseEntity> query)
    {
        return query.Include(x => x.Inventories);
    }
}
