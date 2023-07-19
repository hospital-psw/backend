namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository.AppUsers;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalTreatmentUnitTest
    {

        public MedicalTreatmentService SetupService()
        {
            var logger = new Mock<ILogger<MedicalTreatment>>();
            return new MedicalTreatmentService(logger.Object, new InMemoryUnitOfWork());
        }

        public Mock<IUnitOfWork> SetupUOW()
        {
            var medicalTreatmentRepository = new Mock<IMedicalTreatmentRepository>();
            var userRepository = new Mock<IUserRepository>();
            var roomRepository = new Mock<IRoomRepository>();
            var patientRepository = new Mock<IApplicationPatientRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.MedicalTreatmentRepository).Returns(medicalTreatmentRepository.Object);
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);
            unitOfWork.Setup(u => u.RoomRepository).Returns(roomRepository.Object);
            unitOfWork.Setup(u => u.ApplicationPatientRepository).Returns(patientRepository.Object);
            return unitOfWork;
        }

        private Room SetUpRoom()
        {
            Floor floor1 = new Floor()
            {
                Building = new Building()
                {
                    Address = "Jovana Piperovica 28",
                    Name = "Bolnica"
                },
                Number = FloorNumber.Create(1),
                Purpose = "Bolnica"
            };

            WorkingHours wh1 = new WorkingHours()
            {
                Start = new DateTime(),
                End = new DateTime(1, 1, 1, 23, 0, 0)
            };
            return Room.Create("60", floor1, "Soba za decu", wh1);
        }

        private ApplicationPatient SetUpPatient()
        {
            return new ApplicationPatient()
            {
                FirstName = "Mika",
                LastName = "Mikic",
                Email = "mika@com",
                Hospitalized = true,
                Blocked = true,
                Strikes = 3,
            };
        }

        private MedicalTreatment SetUpMedicalTreatment()
        {
            Room room = SetUpRoom();

            ApplicationPatient patient = SetUpPatient();

            return new MedicalTreatment
            {
                Room = room,
                Doctor = null,
                Patient = patient,
                MedicamentTherapies = new List<MedicamentTherapy>(),
                BloodUnitTherapies = new List<BloodUnitTherapy>(),
                Active = true,
                Report = null,
                End = default(DateTime),
                Start = new DateTime(),
            };
        }

        [Fact]
        public void Finish_treatment()
        {
            var unitOfWork = SetupUOW();
            var medicalTreatmentService = SetupService();
            MedicalTreatment medicalTreatment = SetUpMedicalTreatment();

            MedicalTreatment result = medicalTreatmentService.ReleasePatient(medicalTreatment,"Zavrseno!");

            Assert.Equal(result.Report,"Zavrseno!");
            Assert.Equal(result.Active, false);
            Assert.NotEqual(medicalTreatment.End, default(DateTime));
        }
    }
}
