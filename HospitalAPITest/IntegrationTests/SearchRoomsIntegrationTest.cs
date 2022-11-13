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

    public class SearchRoomsIntegrationTest : BaseIntegrationTest
    {
        public SearchRoomsIntegrationTest(TestDatabaseFactory factory) : base(factory)
        {
        }

        private static RoomsController SetupController(IServiceScope serviceScope)
        {
            return new RoomsController(serviceScope.ServiceProvider.GetRequiredService<IRoomService>());
        }

        [Fact]
        public void Get_searched_rooms()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var searchCriteria = new SearchCriteriaDto(0, 4, "003", "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));

            var result = ((OkObjectResult)controller.Search(searchCriteria)).Value as List<RoomDto>;

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Get_no_searched_rooms()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var searchCriteria = new SearchCriteriaDto(1, 4, "101", "operaciona sala", new DateTime(2022, 11, 10, 12, 0, 0), new DateTime(2022, 11, 10, 12, 12, 0));

            var result = ((OkObjectResult)controller.Search(searchCriteria)).Value as List<RoomDto>;

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
