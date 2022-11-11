namespace HospitalAPITest.Setup
{
    using HospitalAPI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MySql.Data.EntityFrameworkCore.Infrastructure;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;

    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HospitalDbContext>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<HospitalDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "HospitalApiTestDb"));
            return services.BuildServiceProvider();
        }

        //private static string CreateConnectionStringForTest()
        //{
        //    return "Host=localhost;Port=3306;Database=HospitalApiTestDb;Username=root;Password=psw;";
        //}

        private static void InitializeDatabase(HospitalDbContext context)
        {
            context.Database.EnsureCreated();

            //context.MedicamentTherapies.Add(new MedicamentTherapy
            //{
            //    Id = 1,
            //    About = "",
            //    AmountOfMedicament = 10,
            //    DateCreated = DateTime.Now,
            //    DateUpdated = DateTime.Now,
            //    Deleted = false,
            //    End = new DateTime { },
            //    Medicament = new Medicament { Id = 1, Name = "Aspirin", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, Deleted = false, Description = "Nesto protiv bolova", Quantity = 15 },
            //    Start = DateTime.Now,
            //    Type = TherapyType.MEDICAMENT
            //});

            //context.MedicamentTherapies.Add(new MedicamentTherapy
            //{
            //    Id = 2,
            //    About = "",
            //    AmountOfMedicament = 12,
            //    DateCreated = DateTime.Now,
            //    DateUpdated = DateTime.Now,
            //    Deleted = false,
            //    End = new DateTime { },
            //    Medicament = new Medicament { Id = 2, Name = "Panklav", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, Deleted = false, Description = "Gripa", Quantity = 20 },
            //    Start = DateTime.Now,
            //    Type = TherapyType.MEDICAMENT
            //});
            Patient pat = new Patient()
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Id = 1,
                Deleted = false,
                FirstName = "Mika",
                LastName = "Mikic",
                Email = "mika@com",
                Password = "mikica",
                Role = Role.PATIENT,
                Guest = false
            };

            Doctor doc = new Doctor()
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Id = 3,
                Deleted = false,
                FirstName = "Djankarlo",
                LastName = "Rapacoti",
                Email = "djankarlno@asd.com",
                Password = "djani",
                Role = Role.DOCTOR,
                Specialization = Specialization.CARDIOLOGY,
                WorkHours = new WorkingHours()
                {
                    Start = new DateTime(1, 1, 1, 12, 0, 0),
                    End = new DateTime(1, 1, 1, 16, 0, 0)
                }
            };

            Room room = new Room()
            {
                Floor = new Floor()
                {
                    Building = new Building()
                    {
                        Address = "Jovana Piperovica 14",
                        Name = "Radosno detinjstvo"
                    },
                    Number = 69,
                    Purpose = "Krematorijum"
                },
                Number = "6904",
                Purpose = "Soba za kremiranje",
                WorkingHours = new WorkingHours()
                {
                    Start = new DateTime(),
                    End = new DateTime(1, 1, 1, 23, 0, 0)
                },
                
            };


            context.Patients.Add(pat);

            context.Patients.Add(new Patient
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Id = 2,
                Deleted = false,
                FirstName = "Djura",
                LastName = "Djuric",
                Email = "djura@com",
                Password = "djurica",
                Role = Role.PATIENT,
                Guest = false
            }); ;

            context.Appointments.Add(new Appointment
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Id = 4,
                Deleted = false,
                Date = new DateTime(2022, 11, 11, 14, 0, 0),
                Doctor = doc,
                Patient = pat,
                Room = room,
                IsDone = false,
                ExamType = ExaminationType.OPERATION,
                Duration = 30
            });

            context.SaveChanges();
        }
    }
}
