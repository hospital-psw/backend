namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Emailing;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.AppUsers;
    using HospitalLibraryTest.InMemoryRepositories;
    using Microsoft.Extensions.Logging;
    using MimeKit;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static IdentityServer4.Models.IdentityResources;

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
        public void Send_penalty_email ()
        {
            PenaltyMailService service = new PenaltyMailService();

            var result = service.SendEmail("ilija.galin00@gmail.com");

            Assert.NotNull(result);
            Assert.Equal(result.To.First(), new MailboxAddress("ilija.galin00@gmail.com", "ilija.galin00@gmail.com"));
            Assert.Equal(result.From.First(), new MailboxAddress("Hospital PSW Team", "ikiakus@gmail.com"));
            Assert.Equal(result.Subject, "New Working Hours");
        }   


    }
}
