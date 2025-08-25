using Kf.Service.Warehouse.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.Repositories;

public class InventoryRepositoryBase<TDbContext> : RepositoryBase<TDbContext, InventoryEntity>
    where TDbContext : DbContext
{
    protected InventoryRepositoryBase(
        TDbContext dbContext)
        : base(dbContext)
    {
    }
}
