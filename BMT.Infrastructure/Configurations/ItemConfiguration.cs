using BMT.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Orders;
using BMT.Domain.Shared;

namespace BMT.Infrastructure.Configurations
{
    internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("items");

            builder.HasKey(item => item.ItemID);

            builder.Property(item => item.ItemName)
                .HasConversion(itemName => itemName.ThisName, value => new Name(value));

            builder.OwnsOne(item => item.Price);

            builder.Property(item => item.Quantity);

            //builder.HasOne(item => item.Product);
        }
    }
}


//public Guid ItemID { get; init; }
//public Money Price { get; init; }

//public Name ItemName { get; init; }

//public int Quantity { get; internal set; }