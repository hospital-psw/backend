using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.MedicalTreatment;
using HospitalLibrary.Core.Model.Medicament;
using HospitalLibrary.Core.Model.Therapy;
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
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomMap> RoomsMap { get; set; }

        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MedicalTreatment> MedicalTreatments { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<MedicamentTherapy> MedicamentTherapies { get; set; }
        public DbSet<BloodUnitTherapy> BloodUnitTherapies { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }



        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public HospitalDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Floor>().HasData(
                new Floor() { Id = 2 },
                new Floor() { Id = 3 },
                new Floor() { Id = 4 },
                new Floor() { Id = 5 }
            ) ; 
            base.OnModelCreating(modelBuilder);*/
            
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
