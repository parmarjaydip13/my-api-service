using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using Persistence.Extensions;

namespace Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) => builder.Tap(ConfigureDataStructure);

        private static void ConfigureDataStructure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableNames.Products);
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Id).ValueGeneratedNever();
            builder.Property(product => product.ProductName).HasMaxLength(20).IsRequired();
            builder.Property(product => product.Category).IsRequired();
            builder.Property(product => product.Price).IsRequired().HasPrecision(6, 2);

        }
    }
}
