using BMT.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Shopping_Cart;
using BMT.Domain.Shared;
using BMT.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMT.Infrastructure.Configurations
{
    internal sealed class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("shoppingcarts");

            builder.HasKey(shoppingcart => shoppingcart.Id);

            builder.Property(shoppingcart => shoppingcart.StoreOrWarehouseId);

            builder.Property(shoppingcart => shoppingcart.ManagerId);
                //.UseIdentityColumn<Manager>();

            builder.Property(shoppingcart => shoppingcart.ShoppingCartMaxValue)
                .HasConversion(orderPrice => orderPrice.Value, value => new Money(value));

            builder.OwnsOne(shoppingcart => shoppingcart.ShoppingCartTotal);

            builder.Property(shoppingcart => shoppingcart.OrderType)
                .HasConversion<string>();

            builder.OwnsOne(shoppingcart => shoppingcart.TheShoppingCart);
        }
    }
}
