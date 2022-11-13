namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;

    public class VacationRequestsIntegrationTest : BaseIntegrationTest
    {
        public VacationRequestsIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static VacationRequestsController SetupController(IServiceScope serviceScope)
        {
            return new VacationRequestsController(serviceScope.ServiceProvider.GetRequiredService<IVacationRequestsService>());
        }

        [Fact]
        public void Test_get_all_pending ()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAllPending()).Value as List<VacationRequestDto>;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_accept_vacation_request() { 
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.HandleVacationRequest(new VacationRequestDto(1, HospitalLibrary.Core.Model.Enums.VacationRequestStatus.APPROVED, null)) as StatusCodeResult;
            //OkObjectResult okObject = result as OkObjectResult;


            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Test_decline_vacation_request() {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.HandleVacationRequest(new VacationRequestDto(2, HospitalLibrary.Core.Model.Enums.VacationRequestStatus.REJECTED, "ne moze")) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
