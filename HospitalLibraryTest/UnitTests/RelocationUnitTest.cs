namespace HospitalLibraryTest.UnitTests
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
        public void Is_First_Room_Available()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            DateTime? dateTimes = relocationService.IsRoomAvailable(1, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }

        [Fact]
        public void Is_Second_Room_Available()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            DateTime? dateTimes = relocationService.IsRoomAvailable(2, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0));

            Assert.Null(dateTimes);
        }
        [Fact]
        public void Find_Available_Appointments_For_Both_Rooms()
        {
            RelocationService relocationService = new RelocationService(new InMemoryUnitOfWork());

            List<DateTime> dateTimes = relocationService.GetAvailableAppointments(1, 2, new DateTime(2022, 12, 11, 14, 0, 0, 0), new DateTime(2022, 12, 11, 18, 0, 0), 4);

            Assert.NotEmpty(dateTimes);
            Assert.Equal(dateTimes[0], new DateTime(2022, 12, 11, 14, 0, 0));
        }
    }
}
