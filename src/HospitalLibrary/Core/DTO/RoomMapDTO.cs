namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomMapDTO
    {
        public RoomDTO Room { get; set; }
        public double X { get; set; }
        public double Z { get; set; }

        public RoomMapDTO() { }
        public RoomMapDTO(RoomMap roomMap)
        {
            Room = new RoomDTO(roomMap.Room);
            X = roomMap.X;
            Z = roomMap.Z;
        }

    }
}