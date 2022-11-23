namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.Therapy;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitTherapyIntegrationTest : BaseIntegrationTest
    {
        public BloodUnitTherapyIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static BloodUnitTherapyController SetupController(IServiceScope serviceScope)
        {
            return new BloodUnitTherapyController(
                serviceScope.ServiceProvider.GetRequiredService<IBloodUnitTherapyService>(),
                serviceScope.ServiceProvider.GetRequiredService<IBloodUnitService>()
            );
        }

        [Fact]
        public void Create_bloodunit_therapy()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewBloodUnitTherapyDto newBloodUnitTherapyDto = new NewBloodUnitTherapyDto
            {
                BloodUnitId = 1,
                Amount = 5,
                About = "BURAZZ!!!",
                MedicalTreatmentId = 1,
            };

            var result = ((OkObjectResult)controller.Create(newBloodUnitTherapyDto)).Value as BloodUnitTherapyDto;

            Assert.NotNull(result);
            Assert.Equal(BloodType.A_PLUS, result.BloodUnit.BloodType);
            Assert.Equal(18, result.BloodUnit.Amount);

        }
    }
}
