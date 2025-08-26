using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.Repositories;

public class InventoryRepository<TDbContext>
    : RepositoryBase<TDbContext, InventoryEntity>,
        IInventoryRepository
    where TDbContext : DbContext
{
    protected InventoryRepository(
        TDbContext dbContext)
        : base(dbContext)
    {
    }
}
