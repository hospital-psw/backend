namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
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
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());
            RoomService roomService = new RoomService(null, equipmentService, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), -1, 0);

            rooms.ShouldNotBeEmpty();
        }

        [Fact]
        public void Find_no_suitable_rooms()
        {
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());
            RoomService roomService = new RoomService(null, equipmentService, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("101", 1, 4, "operaciona sala", new DateTime(2022, 11, 10, 12, 0, 0), new DateTime(2022, 11, 10, 12, 12, 0), -1, 0);

            rooms.ShouldBeEmpty();
        }
        /*
        [Fact]
        public void Find_suitable_rooms_with_equipment()
        {
            RoomService roomService = new RoomService(null, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 5);

            rooms.ShouldNotBeEmpty();
        }
        [Fact]
        public void Find_no_suitable_rooms_with_equipment()
        {
            RoomService roomService = new RoomService(null, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 15);

            rooms.ShouldBeEmpty();
        }
        */
    }
}
