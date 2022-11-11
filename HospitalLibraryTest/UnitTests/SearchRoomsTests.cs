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
            RoomService roomService = new RoomService(CreateStubRepository());

            List<Room> rooms =roomService.Search("001", 0, 5, "operaciona sala", new DateTime(), new DateTime());

            rooms.ShouldNotBeEmpty();
        }

        [Fact]
        public void Find_no_suitable_rooms()
        {
            RoomService roomService = new RoomService(CreateStubRepository());

            List<Room> rooms = roomService.Search("101", 1, 4, "operaciona sala", new DateTime(), new DateTime());

            rooms.ShouldBeEmpty();
        }

        private static IRoomRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IRoomRepository>();
            var rooms = new List<Room>();

            Floor floor = new Floor(2, new DateTime(), new DateTime(), false, 0, "ortopedija", new Building());
            WorkingHours workingHours = new WorkingHours();
            Room room1 = new Room(14, "001", new DateTime(), new DateTime(), false, floor, workingHours);
            Room room2 = new Room(16, "003", new DateTime(), new DateTime(), false, floor, workingHours);
            rooms.Add(room1);
            rooms.Add(room2);

            stubRepository.Setup(m => m.GetAll()).Returns(rooms);
            return stubRepository.Object;
        }
    }
}
