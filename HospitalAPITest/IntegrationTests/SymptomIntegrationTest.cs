namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers.Examinations;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomIntegrationTest : BaseIntegrationTest
    {
        public SymptomIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        public SymptomController SetupController(IServiceScope scope)
        {
            return new SymptomController(scope.ServiceProvider.GetRequiredService<ISymptomService>());
        }

        [Fact]
        public void Gets_by_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Get(1)).Value as Symptom;

            Assert.NotNull(result);
            Assert.Equal("Glavobolja", result.Name);
        }

        [Fact]
        public void Creates_new_symptom()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.Add(new Symptom { Name = "Sheesh" })).Value as Symptom;

            Assert.NotNull(result);
            Assert.Equal("Sheesh", result.Name);
        }
    }
}
