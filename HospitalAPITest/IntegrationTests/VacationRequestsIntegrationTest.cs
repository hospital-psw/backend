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

            VacationRequestDto request = new VacationRequestDto(1, null, null, null, HospitalLibrary.Core.Model.Enums.VacationRequestStatus.APPROVED, null, null, null);
            var result = controller.HandleVacationRequest(HospitalLibrary.Core.Model.Enums.VacationRequestStatus.APPROVED, 1) as StatusCodeResult;
            //OkObjectResult okObject = result as OkObjectResult;


            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Test_decline_vacation_request() {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.HandleVacationRequest(HospitalLibrary.Core.Model.Enums.VacationRequestStatus.REJECTED, 2) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
