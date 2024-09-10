using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkV4.Persistence.Configurations
{
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.Property(c=>c.ReceiptId).HasMaxLength(16).IsRequired();
            builder.Property(c=>c.Description).HasMaxLength(250);
            builder.Property(c=>c.VehicleId).IsRequired();
            builder.Property(c=>c.CustomerId).IsRequired();
            builder.Property(c=>c.LocationId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c=>c.FirstPrice).IsRequired();
            builder.Property(c=>c.LastPrice).IsRequired();
            builder.Property(c=>c.PriceDiffrence).IsRequired();
            builder.Property(c=>c.FirstDuration).IsRequired();
            builder.Property(c=>c.LastDuration).IsRequired();
            builder.Property(c=>c.FirstDate).IsRequired();
            builder.Property(c=>c.LastDate).IsRequired();
            builder.Property(c=>c.EntryStatus).IsRequired();
        }
    }
}