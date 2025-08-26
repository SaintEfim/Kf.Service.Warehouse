using Autofac;
using Kf.Service.Warehouse.Data.PostgreSql.Context;
using Kf.Service.Warehouse.Data.Repositories;
using Kf.Service.Warehouse.Data.Repositories.Base;

namespace Kf.Service.Warehouse.Data.PostgreSql;

public class WarehouseDataPostgreSqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterType<WarehouseDbContextFactory>()
            .AsSelf()
            .SingleInstance();

        builder.Register(ctx =>
            {
                var factory = ctx.Resolve<WarehouseDbContextFactory>();
                return factory.CreateDbContext();
            })
            .As<WarehouseDbContext>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();
    }
}
