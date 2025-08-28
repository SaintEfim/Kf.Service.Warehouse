using Autofac;
using Kf.Service.Warehouse.Data.Profile;
using Sieve.Services;

namespace Kf.Service.Warehouse.Data;

public class WarehouseDataModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterType<SieveProcessor>()
            .As<ISieveProcessor>();

        builder.RegisterType<ApplicationSieveProcessor>()
            .As<ISieveProcessor>()
            .SingleInstance();
    }
}
