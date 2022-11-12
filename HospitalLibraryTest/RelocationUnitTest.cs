namespace HospitalLibraryTest
{
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RelocationUnitTest
    {
        [Fact]
        public void Find_Available_Appointments_For_First_Room()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            List<DateTime> dateTimes = relocationService.GetAvailableAppointmentsForRoom(1, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 12, 12, 0, 0), 4);

            Assert.NotNull(dateTimes);
            Assert.Equal(new DateTime(2022, 12, 11, 15, 30, 0), dateTimes[0]);
        }
    }
}
