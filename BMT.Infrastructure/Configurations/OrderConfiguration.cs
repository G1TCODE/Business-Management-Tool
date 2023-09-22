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
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(order => order.Id);


            builder.Property(order => order.StoreOrWarehouseId)
                .HasConversion<Guid>();

            builder.Property(order => order.ManagerId)
                .HasConversion<Guid>();

            builder.Property(order => order.DateOrderPlaced);

            builder.Property(order => order.TotalCost)
                .HasConversion(orderPrice => orderPrice.Value, value => new Money(value));

            builder.Property(order => order.OrderStatus);
            //.HasConversion<string>();

            builder.Property(order => order.OrderType);
                //.HasConversion<string>();
        }
    }
}