using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Kf.Service.Warehouse.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()!.GetReferencedAssemblies()
    .Select(Assembly.Load));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<WarehouseDomainModule>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseRouting();
app.MapControllers();

app.Run();
