namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.AppUsers;
    using HospitalLibraryTest.InMemoryRepositories;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctorUnitTest
    {

        public ApplicationDoctorService SetupService()
        {
            var logger = new Mock<ILogger<ApplicationDoctor>>();
            return new ApplicationDoctorService(logger.Object, new InMemoryUnitOfWork());
        }

        public Mock<IUnitOfWork> SetupUOW()
        {
            var doctorRepository = new Mock<IApplicationDoctorRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.ApplicationDoctorRepository).Returns(doctorRepository.Object);
            return unitOfWork;
        }

        [Fact]
        public void Change_doctor_working_hours()
        {
            var unitOfWork = SetupUOW();
            var doctorService = SetupService();

            WorkingHours wh = new WorkingHours(new DateTime(2023,7,1), new DateTime(2023,9,5));

            bool result = doctorService.ChangeDoctorsShift(wh,1);

            Assert.Equal(result, true);

        }


    }
}
