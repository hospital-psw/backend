namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalAPI.Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class BloodExpenditureIntegrationTest : BaseIntegrationTest
    {
        public BloodExpenditureIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static BloodExpenditureController SetupController(IServiceScope serviceScope)
        {
            return new BloodExpenditureController(
                serviceScope.ServiceProvider.GetRequiredService<IBloodExpenditureService>(),
                serviceScope.ServiceProvider.GetRequiredService<IDoctorService>()
            );
        }

        [Fact]
        public void Create_blood_expenditure()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            CreateExpenditureDTO dto = new CreateExpenditureDTO
            {
                DoctorId = 1,
                Reason = "Za decicu",
                Amount = 5,
                BloodType = BloodType.A_PLUS
            };

            var result = ((OkObjectResult)controller.Create(dto)).Value as CreateExpenditureDTO;
            
            Assert.NotNull(result);
            Assert.Equal(5, result.Amount);
            Assert.Equal("Za decicu", result.Reason);
            Assert.Equal(BloodType.A_PLUS, result.BloodType);
        }



        [Theory]
        [ClassData(typeof(BloodExpenditureData))]
        public void Check_blood_expenditure_creation(CreateExpenditureDTO dto, IActionResult expectedResult)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = controller.Create(dto);

            Assert.Equal(expectedResult.GetType(), result.GetType());
        }

        class BloodExpenditureData : TheoryData<CreateExpenditureDTO, IActionResult>
        {
            public BloodExpenditureData()
            {

                Add(new CreateExpenditureDTO(1, BloodType.A_PLUS, 6, "Za decu"), new OkObjectResult(1));
                Add(new CreateExpenditureDTO(2, BloodType.O_PLUS, 4, "Za pacijenta retosa"), new BadRequestObjectResult("Incorrect Data"));
                Add(new CreateExpenditureDTO(), new BadRequestObjectResult("DTO is null"));
                Add(new CreateExpenditureDTO(1, BloodType.O_MINUS, -9, "Za staru baku"), new BadRequestObjectResult("Incorrect data"));
                Add(new CreateExpenditureDTO(1, BloodType.B_PLUS, 9, null), new BadRequestObjectResult("Incorrect data"));
            }
        }

        [Fact]
        public void Calculate_expenditure() 
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            DateRangeDto dto = new DateRangeDto();
            dto.From = Convert.ToDateTime("2022-11-20T12:06:44.3236514");
            dto.To = Convert.ToDateTime("2022-11-21T20:32:35.244Z");

            var result = ((OkObjectResult)controller.CalculateExpenditure(dto)).Value as CalculateDTO;

            Assert.NotNull(result);
            Assert.Equal(7, result.TotalSum);
            Assert.Equal(7, result.APlusAmount);
        }
    }
}
