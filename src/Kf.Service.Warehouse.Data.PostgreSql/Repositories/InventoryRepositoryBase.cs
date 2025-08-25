using Kf.Service.Warehouse.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.PostgreSql.Repositories;

public class InventoryRepositoryBase : InventoryRepositoryBase<DbContext>
{
    public InventoryRepositoryBase(
        DbContext dbContext)
        : base(dbContext)
    {
    }
}
