using BMT.Domain.Shopping_Cart;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Products;
using BMT.Domain.Shared;

namespace BMT.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(product => product.Id);

            builder.OwnsOne(product => product.Name);

            builder.Property(product => product.Category)
                .HasConversion<string>();

            builder.OwnsOne(product => product.UnitPrice);
                //.HasConversion(orderPrice => orderPrice.Value, value => new Money(value));

            builder.Property(product => product.InStock);

            builder.Property(order => order.Rating);
        }
    }
}
