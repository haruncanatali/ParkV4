using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;

namespace ParkV4.Persistence
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, ICurrentUserService currentUserService)
        : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if(_currentUserService!= null)
            {
				foreach (var entry in ChangeTracker.Entries<BaseEntity>())
				{
					switch (entry.State)
					{
						case EntityState.Added:
							entry.Entity.CreatedBy = _currentUserService.UserId;
							entry.Entity.CreatedAt = DateTime.Now;
							break;
						case EntityState.Modified:
							entry.Entity.UpdatedBy = _currentUserService.UserId;
							entry.Entity.UpdatedAt = DateTime.Now;
							break;
					}
				}
			}

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            base.OnModelCreating(builder);
        }      
    }
}