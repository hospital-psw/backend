namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Service.Blood.Core;
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
                serviceScope.ServiceProvider.GetRequiredService<IBloodExpenditureService>()
            );
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
            Assert.Equal(7, result.totalSum);
            Assert.Equal(7, result.APlusAmount);
        }
    }
}
