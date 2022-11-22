namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPI.Dto.Statistics;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatisticsIntegrationTest : BaseIntegrationTest
    {
        public StatisticsIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }
        private static StatisticalController SetupController(IServiceScope serviceScope)
        {
            return new StatisticalController(serviceScope.ServiceProvider.GetRequiredService<IStatisticsService>());
        }

        [Fact]
        public void Gets_Correct_Statistics()
        {
            //If test drops thats because you changed the test DB or the year is no longer 2022!
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetStats()).Value as StatisticsDTO;

            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(0);
            }
            list.Add(1);
            list.Add(0);    //expected result = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}
            IEnumerable<int> expectedList = list;
            StatisticsDTO expected = new StatisticsDTO();
            expected.Chart1 = list; 

            Assert.NotNull(result);
            Assert.Equal(expected.Chart1, result.Chart1);
        }
    }
}
