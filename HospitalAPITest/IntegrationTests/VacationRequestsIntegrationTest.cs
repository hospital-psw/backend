namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Net.NetworkInformation;

    public class VacationRequestsIntegrationTest : BaseIntegrationTest
    {
        public VacationRequestsIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static VacationRequestsController SetupController(IServiceScope serviceScope)
        {
            return new VacationRequestsController(serviceScope.ServiceProvider.GetRequiredService<IVacationRequestsService>(),
                serviceScope.ServiceProvider.GetRequiredService<IAppointmentService>());
        }

        [Fact]
        public void Test_get_all_pending()
        {

            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((ObjectResult)controller.GetAllPending()).Value as List<VacationRequestDto>;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_accept_vacation_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.HandleVacationRequest(new VacationRequestDto(1, HospitalLibrary.Core.Model.Enums.VacationRequestStatus.APPROVED, null)) as StatusCodeResult;
            //OkObjectResult okObject = result as OkObjectResult;


            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Test_decline_vacation_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.HandleVacationRequest(new VacationRequestDto(2, HospitalLibrary.Core.Model.Enums.VacationRequestStatus.REJECTED, "ne moze")) as StatusCodeResult;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Create_vacation_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewVacationRequestDto dto = new NewVacationRequestDto()
            {
                DoctorId = 1,
                From = new DateTime(2022, 11, 19),
                To = new DateTime(2022, 12, 20),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
            };

            var result = ((OkObjectResult)controller.Create(dto)).Value as VacationRequestDto;

            Assert.NotNull(result);
            Assert.Equal("Djankarlo", result.Doctor.FirstName);
            Assert.Equal("Rapacoti", result.Doctor.LastName);
        }

        [Fact]
        public void Dates_are_in_wrong_order()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewVacationRequestDto dto = new NewVacationRequestDto()
            {
                DoctorId = 1,
                From = new DateTime(2022, 12, 20),
                To = new DateTime(2022, 11, 19),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
            };

            var result = controller.Create(dto) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
        }
        [Fact]
        public void Doctor_has_appointments_in_requested_daterange()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            NewVacationRequestDto dto = new NewVacationRequestDto()
            {
                DoctorId = 1,
                From = new DateTime(2022, 11, 10),
                To = new DateTime(2022, 11, 20),
                Status = VacationRequestStatus.WAITING,
                Comment = "",
                Urgent = false,
            };

            var result = controller.Create(dto) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
        }
        [Fact]
        public void Test_get_all_requests_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int doctorId = 1;

            var result = ((OkObjectResult)controller.GetAllRequestsByDoctorId(doctorId)).Value as List<VacationRequestDto>;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_all_waiting_requests_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int doctorId = 1;

            var result = ((OkObjectResult)controller.GetAllWaitingByDoctorId(doctorId)).Value as List<VacationRequestDto>;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_all_accepted_requests_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int doctorId = 1;

            var result = ((OkObjectResult)controller.GetAllApprovedByDoctorId(doctorId)).Value as List<VacationRequestDto>;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_get_all_rejected_requests_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int doctorId = 1;

            var result = ((OkObjectResult)controller.GetAllRejectedByDoctorId(doctorId)).Value as List<VacationRequestDto>;
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_delete_vacation_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int vacationRequestId = 1;
            var result = controller.Delete(vacationRequestId) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public void Doctor_tries_to_delete_nonexistent_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int vacationRequestId = 100;
            var result = controller.Delete(vacationRequestId) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status404NotFound);
        }

        [Fact]
        public void Doctor_tries_to_delete_nonwaiting_request()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            int vacationRequestId = 6;
            var result = controller.Delete(vacationRequestId) as StatusCodeResult;

            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
        }
    }
}
