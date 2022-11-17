namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AllergiesIntegrationTest : BaseIntegrationTest
    {
        public AllergiesIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static AllergiesController SetupController(IServiceScope scope)
        {
            return new AllergiesController(scope.ServiceProvider.GetRequiredService<IAllergiesService>());
        }
        [Fact]
        public void getAllAllergies()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAll()).Value as List<AllergiesDto>;

            Assert.NotNull(result);
            
        }

        [Fact]
        public void Add_new_allergy()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            AllergiesDto dto = new AllergiesDto
            {
                Name="Dust"
            };

            var result = ((OkObjectResult)controller.Add(dto)).Value as Allergies;

            Assert.NotNull(result);
            Assert.Equal("Dust", result.Name);
      
        }
    }
}
