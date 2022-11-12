namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.Therapy;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalTherapyIntegrationTest : BaseIntegrationTest
    {

        public MedicalTherapyIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static MedicamentTherapyController SetupController(IServiceScope serviceScope)
        {
            return new MedicamentTherapyController(
                serviceScope.ServiceProvider.GetRequiredService<IMedicamentTherapyService>(),
                serviceScope.ServiceProvider.GetRequiredService<IMedicamentService>()
            );
        }

        [Fact]
        public void CreateMedicamentTherapy()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewMedicamentTherapyDto newMedicamentTherapyDto = new NewMedicamentTherapyDto
            {
                MedicamentId = 2,
                Amount = 5,
                About = "BURAZZ!!!",
                MedicalTreatmentId = 1,
            };

            var result = ((OkObjectResult)controller.Create(newMedicamentTherapyDto)).Value as MedicamentTherapyDto;

            Assert.NotNull(result);
            Assert.Equal("Panklav", result.Medicament.Name);
            Assert.Equal(415, result.Medicament.Quantity);

        }
    }
}
