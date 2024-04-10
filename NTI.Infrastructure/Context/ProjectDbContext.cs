using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NTI.Domain.Enums;
using NTI.Domain.Models;
using NTI.Domain.Models.Core;

namespace NTI.Infrastructure.Context
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
            if (Database.IsRelational() && Database.GetAppliedMigrations().Any())
            {
                Database.Migrate();
            }
        }


        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerItem> CustomerItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresEnum<ItemCategory>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (EntityEntry<BaseModel> item in ChangeTracker.Entries<BaseModel>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.ModificatedAt = DateTime.Now;
                        //item.Entity.UpdatedBy = _applicationContext?.UserId ?? string.Empty;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedAt = DateTime.Now;
                        //item.Entity.CreatedBy = _applicationContext?.UserId ?? string.Empty;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

