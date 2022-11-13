namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI;
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public void Test()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAllPending()).Value as List<VacationRequestDto>;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_vacation_request() 
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewVacationRequestDto dto = new NewVacationRequestDto()
            {
                DoctorId = 3,
                From = new DateTime(2022, 11, 19),
                To = new DateTime(2022, 12, 20),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
            };

            var result = ((OkObjectResult)controller.Create(dto)).Value as VacationRequestDto;

            Assert.NotNull(result);
        }

        [Fact]
        public void Dates_are_in_wrong_order()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewVacationRequestDto dto = new NewVacationRequestDto()
            {
                DoctorId = 3,
                From = new DateTime(2022, 12, 20) ,
                To = new DateTime(2022, 11, 19),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
            };

            var result = controller.Create(dto) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
        }
    }
}
