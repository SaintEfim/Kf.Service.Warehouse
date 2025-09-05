using Autofac;
using Kf.Service.Warehouse.Data.PostgreSql;
using Kf.Service.Warehouse.Domain.Services.Base;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka;
using Kf.Service.Warehouse.Domain.Services.Base.Kafka.Handlers;
using Kf.Service.Warehouse.Domain.Services.Inventory;

namespace Kf.Service.Warehouse.Domain;

public class WarehouseDomainModule : Module
{
    private static string GetMessageHandlerKey(
        Type type)
    {
        return type.GetInterfaces()
            .Where(x => x.IsClosedTypeOf(typeof(IMessageHandler<>)))
            .Select(x => x.GenericTypeArguments[0])
            .First()
            .Name;
    }

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

        builder.RegisterType<InventoryMessageBus>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IInventoryMessageHandler>()
            .Keyed<IInventoryMessageHandler>(t => GetMessageHandlerKey(t));

        builder.RegisterType<MessageBusHandleManager>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<KafkaTopicCreator>()
            .As<ITopicCreator>()
            .SingleInstance();

        builder.RegisterType<MessageBusInitializationService>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}
