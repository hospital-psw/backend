using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository.Core
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        void Create(Room room);
        bool Update(Room room);
        void Delete(Room room);
        IEnumerable<Room> GetAvailableRooms();
        void Save();
        IEnumerable<Room> GetRoomsWithWorkingHour(int workHourId);
    }
}
