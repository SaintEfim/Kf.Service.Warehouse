using Autofac;
using Kf.Service.Warehouse.Data.PostgreSql.Context;
using Kf.Service.Warehouse.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.PostgreSql;

public class WarehouseDataPostgreSqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<WarehouseDataModule>();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();

        builder.RegisterType<WarehouseDbContextFactory>()
            .AsSelf()
            .SingleInstance();

        builder.Register(c => c.Resolve<WarehouseDbContextFactory>()
                .CreateDbContext())
            .As<WarehouseDbContext>()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
