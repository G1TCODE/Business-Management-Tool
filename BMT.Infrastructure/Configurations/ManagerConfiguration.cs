using BMT.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Users;
using BMT.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMT.Infrastructure.Configurations
{
    internal sealed class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("managers");

            builder.HasKey(manager => manager.Id);

            builder.Property(manager => manager.Name)
                .HasConversion(managerName => managerName.ThisName, value => new Name(value));

            builder.Property(manager => manager.Email)
            .HasConversion(managerName => managerName.value, value => new Email(value));

            builder.Property(manager => manager.Password)
            .HasConversion(managerPassword => managerPassword.value, value => new Password(value));

            builder.Property(manager => manager.ManagerScope);
                //.HasConversion<string>();

            builder.Property(manager => manager.StoreOrWarehouseId);
        }
    }
}
