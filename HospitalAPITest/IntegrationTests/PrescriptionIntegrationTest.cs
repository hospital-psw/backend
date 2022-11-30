namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.Examinations;
    using HospitalAPI.Dto.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrescriptionIntegrationTest : BaseIntegrationTest
    {
        public PrescriptionIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static PrescriptionController SetupController(IServiceScope scope)
        {
            return new PrescriptionController(scope.ServiceProvider.GetRequiredService<IPrescriptionService>());
        }

        [Fact]
        public void Get_by_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetById(1)).Value as PrescriptionDto;

            Assert.NotNull(result);
            Assert.Equal("Aspirin", result.Medicament.Name);
        }

        [Fact]
        public void Creates_prescription()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewPrescriptionDto dto = new NewPrescriptionDto(1, "Za glavobolju", DateTime.Now, DateTime.Now.AddDays(3));

            var result = ((OkObjectResult)controller.Add(dto)).Value as PrescriptionDto;

            Assert.NotNull(result);
            Assert.Equal("Aspirin", result.Medicament.Name);
        }
    }
}
