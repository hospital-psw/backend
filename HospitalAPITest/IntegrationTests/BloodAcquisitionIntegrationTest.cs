﻿namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPI.Dto.Therapy;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
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

    public class BloodAcquisitionIntegrationTest:BaseIntegrationTest
    {

        public BloodAcquisitionIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static BloodAcquisitionController SetupController(IServiceScope serviceScope)
        {
            return new BloodAcquisitionController(
                serviceScope.ServiceProvider.GetRequiredService<IBloodAcquisitionService>(),
                serviceScope.ServiceProvider.GetRequiredService<IDoctorService>()
            ); 
        }

        [Fact]
        public void Create_blood_acquisition()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);


            CreateAcquisitionDTO dto = new CreateAcquisitionDTO
            {
                Date = DateTime.Now,
                DoctorId = 1,
                Reason = "Bol u uvetu",
                Amount = 5,
                BloodType = BloodType.A_PLUS
            };

            var result = ((OkObjectResult)controller.Create(dto)).Value as CreateAcquisitionDTO;

            Assert.NotNull(result);
            Assert.Equal(5, result.Amount);
            Assert.Equal("Bol u uvetu", result.Reason);
            Assert.Equal(BloodType.A_PLUS, result.BloodType);
            
            
        }

        [Theory]
        [ClassData(typeof(BloodAcquisitionData))]
        public void Release_patient_from_treatment(CreateAcquisitionDTO dto, IActionResult expectedResult)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.Create(dto);

            Assert.Equal(expectedResult.GetType(), result.GetType());
        }

        class BloodAcquisitionData : TheoryData<CreateAcquisitionDTO, IActionResult>
        {
            public BloodAcquisitionData()
            {
                Add(new CreateAcquisitionDTO(1, DateTime.Now, BloodType.A_MINUS, 6, "Bol u peti",BloodRequestStatus.PENDING), new OkObjectResult(1));
                Add(new CreateAcquisitionDTO(2, DateTime.Now, BloodType.O_PLUS, 4, "Bol u peti", BloodRequestStatus.PENDING), new BadRequestObjectResult("Doctor not found"));
                Add(null, new BadRequestObjectResult("Incorrect data, please enter valid data"));
                Add(new CreateAcquisitionDTO(1, DateTime.Now, BloodType.O_MINUS, -9, "Bol u glavenom predelu", BloodRequestStatus.PENDING), new BadRequestObjectResult("Incorrect data, please enter valid data"));
                Add(new CreateAcquisitionDTO(1, DateTime.Now, BloodType.B_PLUS, -9, null, BloodRequestStatus.PENDING), new BadRequestObjectResult("Incorrect data, please enter valid data"));
            }
        }

    }
}