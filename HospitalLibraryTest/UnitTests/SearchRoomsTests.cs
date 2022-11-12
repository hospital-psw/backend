namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using Moq;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SearchRoomsTests
    {
        [Fact]
        public void Find_suitable_rooms()
        {
            /*RoomService roomService = new RoomService(CreateStubRepository());

            List<Room> rooms =roomService.Search("003", 0, 4  , "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));

            rooms.ShouldNotBeEmpty();*/
        }

        [Fact]
        public void Find_no_suitable_rooms()
        {
            /*RoomService roomService = new RoomService(CreateStubRepository());

            List<Room> rooms = roomService.Search("101", 1, 4, "operaciona sala", new DateTime(2022, 11, 10, 12, 0, 0), new DateTime(2022, 11, 10, 12, 12, 0));

            rooms.ShouldBeEmpty();*/
        }

        private static IRoomRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IRoomRepository>();
            var rooms = new List<Room>();

            
            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));
            Building building = new Building(4, new DateTime(), new DateTime(), false, "Hospital2", "Janka Cmelika 1");
            Floor floor = new Floor(2, new DateTime(), new DateTime(), false, 0, "ortopedija", building);
            Room room1 = new Room(14, "001", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);
            Room room2 = new Room(16, "003", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);
            rooms.Add(room1);
            rooms.Add(room2);

            stubRepository.Setup(m => m.GetAll()).Returns(rooms);
            return stubRepository.Object;
        }
    }
}
