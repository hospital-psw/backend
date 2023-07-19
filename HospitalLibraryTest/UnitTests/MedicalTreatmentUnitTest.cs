namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Model.VacationRequests;
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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.MedicalTreatmentRepository).Returns(medicalTreatmentRepository.Object);
            return unitOfWork;
        }

        private MedicalTreatment SetUpMedicalTreatment()
        {
            return new MedicalTreatment
            {
                Room = null,
                Doctor = null,
                Patient = null,
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

            medicalTreatmentService.SetTreatmentFinished(medicalTreatment,"Zavrseno!");

            Assert.Equal(medicalTreatment.Report,"Zavrseno!");
            Assert.Equal(medicalTreatment.Active, false);
            Assert.NotEqual(medicalTreatment.End, default(DateTime));
        }
    }
}
