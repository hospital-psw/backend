namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.AppUsers;
    using HospitalLibrary.Util;
    using HospitalLibraryTest.InMemoryRepositories;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecommendDoctorTest
    {
        [Fact]
        public void Find_suitable_doctors()
        {
            ApplicationDoctorService applicationDoctorService = new ApplicationDoctorService(null, new InMemoryUnitOfWork());

            IEnumerable<ApplicationDoctor> appDoctors = applicationDoctorService.RecommendDoctors();
            ApplicationDoctor applicationDoctor = new ApplicationDoctor("Marko", "Markovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            appDoctors.ShouldNotBeEmpty();
            appDoctors.Contains(applicationDoctor);

        }
    }
}
