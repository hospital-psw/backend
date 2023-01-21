using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.ApplicationUser;
using HospitalLibrary.Core.Model.ApplicationUser;
using HospitalLibrary.Core.Model.Blood;
using HospitalLibrary.Core.Model.Blood.BloodManagment;
using HospitalLibrary.Core.Model.Events;
using HospitalLibrary.Core.Model.Events.Scheduling;
using HospitalLibrary.Core.Model.Events.Scheduling.Root;
using HospitalLibrary.Core.Model.Examinations;
using HospitalLibrary.Core.Model.MedicalTreatment;
using HospitalLibrary.Core.Model.Medicament;
using HospitalLibrary.Core.Model.Therapy;
using HospitalLibrary.Core.Model.VacationRequests;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        //private static ProjectConfiguration _configuration;

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomMap> RoomsMap { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MedicalTreatment> MedicalTreatments { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<MedicamentTherapy> MedicamentTherapies { get; set; }
        public DbSet<BloodUnitTherapy> BloodUnitTherapies { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<BloodAcquisition> BloodAcquisitions { get; set; }
        public DbSet<BloodExpenditure> BloodExpenditures { get; set; }
        public DbSet<RelocationRequest> RelocationRequests { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<ApplicationPatient> ApplicationPatients { get; set; }
        public DbSet<ApplicationDoctor> ApplicationDoctors { get; set; }
        public DbSet<RenovationDetails> RenovationDetails { get; set; }
        public DbSet<RenovationRequest> RenovationRequests { get; set; }
        public DbSet<Consilium> Consiliums { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<Anamnesis> Anamneses { get; set; }

        public DbSet<RenovationEvent> RenovationEvents { get; set; }
        public DbSet<AppointmentEvent> AppointmentEvents { get; set; }

        public DbSet<ExaminationStarted> ExaminationStartedEvents { get; set; }

        public DbSet<SymptomsChanged> SymptomsChangedEvents { get; set; }

        public DbSet<DescriptionCreated> DescriptionCreatedEvents { get; set; }

        public DbSet<PrescriptionCreated> PrescriptionCreatedEvents { get; set; }

        public DbSet<PrescriptionRemoved> PrescriptionRemovedEvents { get; set; }

        public DbSet<ExaminationFinished> ExaminationFinishedEvents { get; set; }

        public DbSet<ExaminationEvent> ExaminationEvents { get; set; }

        public DbSet<BloodAddition> BloodAdditions { get; set; }

        public DbSet<AppointmentSchedulingRoot> AppointmentRoots { get; set; }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }
        public DbSet<AppointmentScheduled> AppointmentScheduledEvents { get; set; }
        public DbSet<AppointmentSelected> AppointmentSelectedEvents { get; set; }
        public DbSet<BackClicked> BackClickedEvents { get; set; }
        public DbSet<DateSelected> DateSelectedEvents { get; set; }
        public DbSet<DoctorSelected> DoctorSelectedEvents { get; set; }
        public DbSet<NextClicked> NextClickedEvents { get; set; }
        public DbSet<SessionStarted> SessionStartedEvents { get; set; }
        public DbSet<SpecializationSelected> SpecializationSelectedEvents { get; set; }


        public HospitalDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RenovationEvent>().ToTable(nameof(RenovationEvent));
            modelBuilder.Entity<AppointmentEvent>().ToTable(nameof(AppointmentEvent));
            modelBuilder.Entity<ExaminationStarted>().ToTable("ExaminationStartedEvents");
            modelBuilder.Entity<ExaminationFinished>().ToTable("ExaminationFinishedEvents");
            modelBuilder.Entity<SymptomsChanged>().ToTable("SymptomsChangedEvents");
            modelBuilder.Entity<DescriptionCreated>().ToTable("DescriptionCreatedEvents");
            modelBuilder.Entity<PrescriptionCreated>().ToTable("PrescriptionCreatedEvents");
            modelBuilder.Entity<PrescriptionRemoved>().ToTable("PrescriptionRemovedEvents");
            modelBuilder.Entity<ExaminationEvent>().ToTable("ExaminationEvents");

            modelBuilder.Entity<AppointmentScheduled>().ToTable("AppointmentScheduledEvents");
            modelBuilder.Entity<AppointmentSelected>().ToTable("AppointmentSelectedEvents");
            modelBuilder.Entity<BackClicked>().ToTable("BackClickedEvents");
            modelBuilder.Entity<DateSelected>().ToTable("DateSelectedEvents");
            modelBuilder.Entity<DoctorSelected>().ToTable("DoctorSelectedEvents");
            modelBuilder.Entity<NextClicked>().ToTable("NextClickedEvents");
            modelBuilder.Entity<SessionStarted>().ToTable("SessionStartedEvents");
            modelBuilder.Entity<SpecializationSelected>().ToTable("SpecializationSelectedEvents");
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

    }
}
