using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        //private static ProjectConfiguration _configuration;

        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public HospitalDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );
            base.OnModelCreating(modelBuilder);
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer("Server=tcp:pswhospital.database.windows.net,1433;Initial Catalog=pswhospital;Persist Security Info=False;User ID=pswadmin;Password=zmajOdSipova1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}