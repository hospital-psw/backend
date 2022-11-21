using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service.Core
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        void Create(Room room);
        bool Update(Room room);
        void Delete(Room room);
        IEnumerable<Room> GetAvailable();
        List<Room> Search(string roomNumber, int floorNumber, int buildingId, string purpose, DateTime start, DateTime end, int equipmentType, int quantity);
    }
}
