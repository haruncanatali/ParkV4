using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkV4.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c=>c.IdentityNumber).HasMaxLength(11).IsRequired();
            builder.Property(c=>c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.Surname).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.TelephoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Photo).HasMaxLength(250).IsRequired();
        }
    }
}