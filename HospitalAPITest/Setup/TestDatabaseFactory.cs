namespace HospitalAPITest.Setup
{
    using HospitalAPI;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class TestDatabaseFactory : WebApplicationFactory<Startup>
    {
        public HospitalDbContext dbContext;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HospitalDbContext>();
                dbContext = db;
                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<HospitalDbContext>(opt => opt.UseSqlServer(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalTestBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private static void InitializeDatabase(HospitalDbContext context)
        {
            context.Database.EnsureDeleted();
            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE VacationRequests");
            context.Database.EnsureCreated();

            //context.Database.ExecuteSqlRaw("DELETE FROM ");
            //context.MedicamentTherapies.Add(new MedicamentTherapy
            //{
            //    Id = 1,
            //    About = "",
            //    AmountOfMedicament = 10,
            //    DateCreated = DateTime.Now,
            //    DateUpdated = DateTime.Now,
            //    DELETE FROMd = false,
            //    End = new DateTime { },
            //    Medicament = new Medicament { Id = 1, Name = "Aspirin", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DELETE FROMd = false, Description = "Nesto protiv bolova", Quantity = 15 },
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
            //    DELETE FROMd = false,
            //    End = new DateTime { },
            //    Medicament = new Medicament { Id = 2, Name = "Panklav", DateCreated = DateTime.Now, DateUpdated = DateTime.Now, DELETE FROMd = false, Description = "Gripa", Quantity = 20 },
            //    Start = DateTime.Now,
            //    Type = TherapyType.MEDICAMENT
            //});
            Patient pat = new Patient()
            {
                FirstName = "Mika",
                LastName = "Mikic",
                Email = "mika@com",
                Password = "mikica",
                Role = Role.PATIENT,
                Hospitalized = true
            };

            Patient pat2 = new Patient
            {
                FirstName = "Djura",
                LastName = "Djuric",
                Email = "djura@com",
                Password = "djurica",
                Role = Role.PATIENT,
                Hospitalized = true,
            };

            List<Patient> patients = new List<Patient>();
            patients.Add(pat);
            patients.Add(pat2);

            Doctor doc = new Doctor()
            {
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
                Patients = patients

            };

            BloodExpenditure expenditure = new BloodExpenditure()
            {
                Doctor = doc,
                BloodType = BloodType.A_PLUS,
                Amount = 7,
                Reason = "blabla",
                Date = Convert.ToDateTime("2022-11-21T12:06:44.3236514")
            };

            context.BloodExpenditures.Add(expenditure);
            context.Patients.Add(pat);

            context.Patients.Add(pat2);

            context.Appointments.Add(new Appointment
            {
                Date = new DateTime(2022, 11, 11, 14, 0, 0),
                Doctor = doc,
                Patient = pat,
                Room = room,
                IsDone = false,
                ExamType = ExaminationType.OPERATION,
                Duration = 30
            });

            context.Medicaments.Add(new Medicament
            {
                Name = "Aspirin",
                Description = "Nesto protiv bolova",
                Quantity = 15
            });

            context.Medicaments.Add(new Medicament
            {
                Name = "Panklav",
                Description = "Antibiotik za decu i odrasle",
                Quantity = 420
            });

            context.MedicalTreatments.Add(new MedicalTreatment
            {
                Room = room,
                Doctor = doc,
                Patient = pat,
                MedicamentTherapies = new List<MedicamentTherapy>(),
                BloodUnitTherapies = new List<BloodUnitTherapy>(),
                Active = true,
                Report = null,
                End = default(DateTime),
                Start = new DateTime(),
            }); ;

            context.MedicalTreatments.Add(new MedicalTreatment
            {
                Room = room,
                Doctor = doc,
                Patient = pat2,
                MedicamentTherapies = new List<MedicamentTherapy>(),
                BloodUnitTherapies = new List<BloodUnitTherapy>(),
                Active = false,
                Report = "izvestaj brateeeee",
                End = default(DateTime),
                Start = new DateTime(),
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Deleted = false,
                Doctor = doc,
                From = new DateTime(2022, 11, 25, 0, 0, 0),
                To = new DateTime(2022, 12, 11, 0, 0, 0),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = true,
                ManagerComment = ""
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Deleted = false,
                Doctor = doc,
                From = new DateTime(2022, 12, 12, 0, 0, 0),
                To = new DateTime(2022, 12, 15, 0, 0, 0),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = true,
                ManagerComment = ""
            });


            //for equipment controller
            Room equipmentRoom = new Room()
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
            context.Equipments.Add(new Equipment
            {
                EquipmentType = EquipmentType.BED,
                Quantity = 8,
                Room = equipmentRoom
            });
            context.Equipments.Add(new Equipment
            {
                EquipmentType = EquipmentType.SCISSORS,
                Quantity = 10,
                Room = equipmentRoom
            });
            context.Equipments.Add(new Equipment
            {
                EquipmentType = EquipmentType.NEEDLE,
                Quantity = 20,
                Room = equipmentRoom
            });
            context.Equipments.Add(new Equipment
            {
                EquipmentType = EquipmentType.BANDAGE,
                Quantity = 5,
                Room = equipmentRoom
            });

            BloodAcquisition aquisition1 = new BloodAcquisition
            {
                //Id = 1,
                BloodType = BloodType.A_MINUS,
                Amount = 1,
                Status = BloodRequestStatus.ACCEPTED
            };

            BloodAcquisition aquisition2 = new BloodAcquisition
            {
                //Id = 2,
                BloodType = BloodType.O_PLUS,
                Amount = 2,
                Status = BloodRequestStatus.DECLINED,
            };

            BloodAcquisition aquisition3 = new BloodAcquisition
            {
                //Id = 2,
                BloodType = BloodType.O_PLUS,
                Amount = 2,
                Status = BloodRequestStatus.PENDING
            };

            context.BloodAcquisitions.Add(aquisition1);
            context.BloodAcquisitions.Add(aquisition2);
            context.BloodAcquisitions.Add(aquisition3);

            Building building = new Building()
            {
                Address = "Janka Cmelika 1",
                Name = "Hospital2"
            };

            context.Buildings.Add(building);

            Floor floor = new Floor()
            {
                Building = building,
                Number = 0,
                Purpose = "ortopedija"
            };

            context.Floors.Add(floor);

            WorkingHours workingHours = new WorkingHours()
            {
                Start = new DateTime(2022, 11, 10, 4, 0, 0),
                End = new DateTime(2022, 11, 10, 7, 0, 0)
            };

            context.WorkingHours.Add(workingHours);

            context.Rooms.Add(new Room
            {
                Floor = floor,
                Number = "003",
                Purpose = "ordinacija",
                WorkingHours = workingHours
            });

            context.BloodUnits.Add(new BloodUnit
            {
                BloodType = BloodType.A_PLUS,
                Amount = 23
            });

            context.Allergies.Add(new Allergies
            {
                Name = "kupus"

            });

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2022, 12, 12, 0, 0, 0),
                To = new DateTime(2022, 12, 15, 0, 0, 0),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = true,
                ManagerComment = ""

>>>>>>>>> Temporary merge branch 2

            context.ApplicationPatients.Add(new ApplicationPatient
                ("Nikola", "Grbovic", new DateTime(2000, 11, 22), Gender.MALE, false, BloodType.A_PLUS));
            context.ApplicationPatients.Add(new ApplicationPatient
                ("Marko", "Matkovic", new DateTime(2000, 11, 22), Gender.MALE, false, BloodType.A_PLUS));
            context.ApplicationPatients.Add(new ApplicationPatient
                ("Fosilka", "Fosilovic", new DateTime(1930, 11, 26), Gender.FEMALE, false, BloodType.O_PLUS));
            context.ApplicationDoctors.Add(new ApplicationDoctor
                ("Galina", "Gavanski", new DateTime(1980, 5, 1), Gender.FEMALE, Specialization.GENERAL, null, null));
            context.ApplicationDoctors.Add(new ApplicationDoctor
                ("Lik", "Beson", new DateTime(1992, 5, 1), Gender.MALE, Specialization.NEUROLOGY, null, null));

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2023, 1, 12, 0, 0, 0),
                To = new DateTime(2023, 1, 22, 0, 0, 0),
                Status = VacationRequestStatus.APPROVED,
                Comment = "",
                Urgent = true,
                ManagerComment = ""
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2023, 2, 12, 0, 0, 0),
                To = new DateTime(2023, 2, 22, 0, 0, 0),
                Status = VacationRequestStatus.APPROVED,
                Comment = "",
                Urgent = false,
                ManagerComment = ""
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2023, 3, 12, 0, 0, 0),
                To = new DateTime(2023, 3, 22, 0, 0, 0),
                Status = VacationRequestStatus.REJECTED,
                Comment = "",
                Urgent = false,
                ManagerComment = "Mnogo trazis baca"
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2023, 4, 12, 0, 0, 0),
                To = new DateTime(2023, 4, 22, 0, 0, 0),
                Status = VacationRequestStatus.REJECTED,
                Comment = "",
                Urgent = true,
                ManagerComment = "Ti ga bas pretera"
            });

            context.VacationRequests.Add(new VacationRequest
            {
                Doctor = doc,
                From = new DateTime(2023, 5, 12, 0, 0, 0),
                To = new DateTime(2023, 5, 22, 0, 0, 0),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
                ManagerComment = ""
            });


            context.SaveChanges();

        }
    }
}
