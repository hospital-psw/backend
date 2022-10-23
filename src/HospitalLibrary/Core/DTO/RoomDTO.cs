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
        public FloorDTO Floor { get; set; }
        public BuildingDTO Building { get; set; }
        public string Purpose { get; set; }
        public double X { get; set; }
        public double Z { get; set; }

        public RoomDTO(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            Floor = new FloorDTO(room.Floor);
            Building = new BuildingDTO(room.Building);
            Purpose = room.Purpose;
            X = room.X;
            Z = room.Z;
        }

    }
}
