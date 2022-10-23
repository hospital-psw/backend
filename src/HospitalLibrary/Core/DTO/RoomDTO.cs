namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomDTO
    {
        public string Number { get; set; }
        public int FloorId { get; set; }
        public int BuildingId { get; set; }
        public string Purpose { get; set; }

        public RoomDTO(Room room)
        {
            Number = room.Number;
            FloorId = room.Floor.Id;
            BuildingId = room.Building.Id;
            Purpose = room.Purpose;
        }

    }
}
