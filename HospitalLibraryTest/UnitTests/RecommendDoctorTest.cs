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

            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 10, 0, 0), new DateTime(2022, 11, 10, 16, 0, 0));
            ApplicationDoctor applicationDoctor = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, workingHours, null);
            appDoctors.ShouldNotBeEmpty();
            appDoctors.Count().ShouldBe(1);
            appDoctors.First().FirstName.ShouldBe(applicationDoctor.FirstName);


        }
    }
}
