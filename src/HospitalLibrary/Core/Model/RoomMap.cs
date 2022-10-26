namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.DTO;

    public class RoomMap : Entity
    {
        public Room Room { get; set; }
        public double X { get; set; }
        public double Z { get; set; }

        public RoomMap()
        {
        }

        public RoomMap(RoomMapDTO dto)
        {
            Room = new Room(dto.Room);
            X = dto.X;
            Z = dto.Z;
        }
    }
}