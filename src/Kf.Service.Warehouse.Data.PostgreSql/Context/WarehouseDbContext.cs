using Kf.Service.Warehouse.Data.Models;
using Kf.Service.Warehouse.Data.PostgreSql.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Kf.Service.Warehouse.Data.PostgreSql.Context;

public sealed class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(
        DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<InventoryEntity> Inventories { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WarehouseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
