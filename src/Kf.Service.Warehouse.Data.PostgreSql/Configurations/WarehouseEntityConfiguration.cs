using Kf.Service.Warehouse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kf.Service.Warehouse.Data.PostgreSql.Configurations;

internal sealed class WarehouseEntityConfiguration : IEntityTypeConfiguration<WarehouseEntity>
{
    public void Configure(
        EntityTypeBuilder<WarehouseEntity> builder)
    {
        builder.HasMany(x => x.Inventories)
            .WithOne()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
