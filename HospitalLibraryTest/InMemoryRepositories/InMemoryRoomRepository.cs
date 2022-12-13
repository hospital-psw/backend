namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryRoomRepository : IRoomRepository
    {
        public void Create(Room room)
        {
            throw new NotImplementedException();
        }

        public void Delete(Room room)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAll()
        {
            var rooms = new List<Room>();

            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));
            Building building = new Building(4, new DateTime(), new DateTime(), false, "Hospital2", "Janka Cmelika 1");
            Floor floor = new Floor(2, new DateTime(), new DateTime(), false, FloorNumber.Create(0), "ortopedija", building);
            Room room1 = Room.Create("001", floor, "ordinacija", workingHours);
            room1.SetId(14);
            Room room2 = Room.Create("003", floor, "ordinacija", workingHours);
            room1.SetId(16);
            rooms.Add(room1);
            rooms.Add(room2);

            return rooms;
        }

        public IEnumerable<Room> GetAvailableRooms()
        {
            throw new NotImplementedException();
        }

        public Room GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetRoomsWithWorkingHour(int workHourId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
