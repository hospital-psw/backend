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


        public RoomMap() {}
    }
}
