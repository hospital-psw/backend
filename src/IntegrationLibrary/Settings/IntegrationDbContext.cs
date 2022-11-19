namespace IntegrationLibrary.Settings
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.News;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegrationDbContext : DbContext
    {
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<News> News { get; set; }
        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }
        public IntegrationDbContext() { }

        public override int SaveChanges()
        {

            IEnumerable<EntityEntry> entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (EntityEntry entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).DateUpdated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).DateCreated = DateTime.Now;
                    ((Entity)entityEntry.Entity).Deleted = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
