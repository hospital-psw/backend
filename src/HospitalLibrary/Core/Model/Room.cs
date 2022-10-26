using HospitalLibrary.Core.DTO;


namespace HospitalLibrary.Core.Model
{
    public class Room : Entity
    {
        public string Number { get; set; }
        public Floor Floor { get; set; }
        public Building Building { get; set; }
        public string Purpose { get; set; }

        public Room()
        {
        }

        public Room(RoomDTO dto)
        {
            Number = dto.Number;
            Purpose = dto.Purpose;
        }

    }
}