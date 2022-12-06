namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.DTO;

    public class RoomMap : Entity
    {
        public Room Room { get; set; }
        public double X { get; set; }
        public double Z { get; set; }
        public double width { get; set; }
        public double depth { get; set; }


        public RoomMap() { }

        public RoomMap(Room room, double x, double z, double width, double depth) {
            Room = room;
            X = x;
            Z = z;
            this.width = width;
            this.depth = depth;
        }
        public static RoomMap Create(Room room, double x, double z, double width, double depth) {
            return new RoomMap(room, x, z, width, depth);
        }

        public void Delete() {
            Deleted = true;
        }
    }
}
