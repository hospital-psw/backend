namespace HospitalLibraryTest.UnitTests
{
    using CSharpFunctionalExtensions;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibraryTest.InMemoryRepositories;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    public class SearchEquipmentTest
    {
        [Fact]
        public void Find_suitable_rooms_with_equipment()
        {
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());
            RoomService roomService = new RoomService(null, equipmentService, new InMemoryUnitOfWork());
            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 5);
            List<Room> result = equipmentService.SearchRooms(rooms, 0, 5);
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(1);
            result.First().Number.ShouldBe("003");
            result.First().Purpose.ShouldBe("ordinacija");
            
        }
        [Fact]
        public void Find_no_suitable_rooms_with_equipment()
        {
            EquipmentService equipmentService = new EquipmentService(null, new InMemoryUnitOfWork());
            RoomService roomService = new RoomService(null, equipmentService, new InMemoryUnitOfWork());

            List<Room> rooms = roomService.Search("003", 0, 4, "ordinacija", new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0), 0, 15);
            List<Room> result = equipmentService.SearchRooms(rooms, 0, 15);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
            result.Count.ShouldBe(0);
        }
    }
}
