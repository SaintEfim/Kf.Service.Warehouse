using Microsoft.Extensions.Configuration;

namespace Kf.Service.Warehouse.Data.PostgreSql.Context;

public sealed class WarehouseDbContextFactory : PostgreSqlDbContextFactoryBase<WarehouseDbContext>
{
    public WarehouseDbContextFactory(
        IConfiguration configuration)
        : base(configuration)
    {
    }

    protected override string ConnectionString => "ServiceDB";
}
