namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SearchEquipmentTest
    {
        [Fact]
        public void Find_suitable_rooms_with_equipment()
        {
            RoomService roomService = new RoomService(null, new InMemoryUnitOfWork());
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 5);
            List<Room> result = equipmentService.SearchRooms(rooms, 0, 5);
            result.ShouldNotBeEmpty();
        }
        [Fact]
        public void Find_no_suitable_rooms_with_equipment()
        {
            RoomService roomService = new RoomService(null, new InMemoryUnitOfWork());
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 15);
            List<Room> result = equipmentService.SearchRooms(rooms, 0, 15);

            result.ShouldBeEmpty();
        }
    }
}
