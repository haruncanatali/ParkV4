using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkV4.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(c=>c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.Description).HasMaxLength(250).HasDefaultValue("Yok");
            builder.Property(c=>c.CompanyId).IsRequired();
        }
    }
}