using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkV4.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(c=>c.VehicleType).IsRequired();
            builder.Property(c=>c.Plate).HasMaxLength(12).IsRequired();
            builder.Property(c=>c.Color).HasMaxLength(75).IsRequired();
            builder.Property(c=>c.BrandId).IsRequired();
            builder.Property(c=>c.ModelId).IsRequired();
        }
    }
}