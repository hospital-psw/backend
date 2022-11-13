namespace HospitalAPITest.IntegrationTests
{
    using HospitalAPI.Controllers;
    using HospitalAPITest.Setup;
    using HospitalLibrary.Core.Service.Core;
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
            
        }
    }
}
