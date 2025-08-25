using Kf.Service.Warehouse.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.PostgreSql.Repositories;

public class WarehouseRepositoryBase : WarehouseRepositoryBase<DbContext>
{
    public WarehouseRepositoryBase(
        DbContext dbContext)
        : base(dbContext)
    {
    }
}
