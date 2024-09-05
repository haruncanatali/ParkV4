using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkV4.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c=>c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.Surname).HasMaxLength(50).IsRequired();
            builder.Property(c=>c.Photo).HasMaxLength(250).IsRequired();
            builder.Property(c=>c.Username).HasMaxLength(15).IsRequired();
            builder.Property(c=>c.Password).HasMaxLength(12).IsRequired();
            builder.Property(c=>c.Email).HasMaxLength(150).IsRequired();
            builder.Property(c=>c.TelephoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(c=>c.CompanyId).IsRequired();
        }
    }
}