using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Locations;
using BMT.Domain.Shared;
using BMT.Domain.Orders;
using Azure.Core;
using BMT.Domain.Users;
using System.Threading;
using BMT.Domain.Orders.Events;

namespace BMT.Infrastructure.Configurations
{
    internal sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("stores");

            builder.HasKey(store => store.Id);

            builder.OwnsOne(store => store.Address);

            builder.Property(store => store.Name)
                .HasConversion(storeName => storeName.ThisName, value => new Name(value));

            builder.Property(store => store.ManagerId);

            //builder.Property(store => store.OpeningDate)
            //    .HasConversion<string>();

        }
    }
}
