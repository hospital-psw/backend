namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;

    public class RoomDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public FloorDTO Floor { get; set; }
        public BuildingDTO Building { get; set; }
        public string Purpose { get; set; }

        public RoomDTO() { }
        public RoomDTO(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            Floor = new FloorDTO(room.Floor);
            Building = new BuildingDTO(room.Building);
            Purpose = room.Purpose;
        }

    }
}