namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomDetailsDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Purpose { get; set; }

        public RoomDetailsDTO(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            Purpose = room.Purpose;
        }
    }
}
