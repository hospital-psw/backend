namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEquipmentService
    {
        List<Equipment> GetForRoom(int roomId);
        Equipment Get(int id);
        List<Room> SearchRooms(List<Room> rooms, int equipmentType, int quantity);
    }
}
