namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RelocationIntegrationTest : BaseIntegrationTest
    {

        public RelocationIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static RelocationController SetupController(IServiceScope serviceScope)
        {
            return new RelocationController(serviceScope.ServiceProvider.GetRequiredService<IRelocationService>());
        }

        [Fact]
        public void Test_Create_Relocation()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            
            var result = controller.Create() as StatusCodeResult;
           
        }
    }
}
