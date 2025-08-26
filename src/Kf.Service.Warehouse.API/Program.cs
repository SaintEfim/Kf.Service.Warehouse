using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Kf.Service.Warehouse.Data.PostgreSql;
using Kf.Service.Warehouse.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<WarehouseDomainModule>();
    containerBuilder.RegisterModule<WarehouseDataPostgreSqlModule>();

    containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
        .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract)
        .As<Profile>()
        .SingleInstance();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapControllers();

app.Run();
