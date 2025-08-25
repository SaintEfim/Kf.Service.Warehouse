using Kf.Service.Warehouse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kf.Service.Warehouse.Data.PostgreSql.Configurations;

internal sealed class InventoryEntityConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(
        EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.ToTable("Inventories");
    }
}
