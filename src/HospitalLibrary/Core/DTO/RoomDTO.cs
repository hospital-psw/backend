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
        public int Id { get; set; }
        public string Number { get; set; }
        public int FloorId { get; set; }
        public int BuildingId { get; set; }
        public string Purpose { get; set; }
        public double X { get; set; }
        public double Z { get; set; }

        public RoomDTO(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            FloorId = room.Floor.Id;
            BuildingId = room.Building.Id;
            Purpose = room.Purpose;
            X = room.X;
            Z = room.Z;
        }

    }
}
