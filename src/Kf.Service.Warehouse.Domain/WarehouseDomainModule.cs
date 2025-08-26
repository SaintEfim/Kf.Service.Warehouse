using Autofac;
using Kf.Service.Warehouse.Data.PostgreSql;
using Kf.Service.Warehouse.Domain.Services.Base;

namespace Kf.Service.Warehouse.Domain;

public class WarehouseDomainModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<WarehouseDataPostgreSqlModule>();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataProvider<>))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataManager<>))
            .AsImplementedInterfaces();
    }
}
