namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FloorDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Purpose { get; set; }
        public List<RoomDTO> Rooms { get; set; }

        public FloorDTO(Floor floor)
        {
            Id = floor.Id;
            Number = floor.Number;
            Purpose = floor.Purpose;
            Rooms = new List<RoomDTO>();
        }

    }
}
