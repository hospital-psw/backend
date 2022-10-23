namespace HospitalLibrary.Core.DTO
{
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

        public RoomDetailsDTO(int id, string number, string purpose)
        {
            Id = id;
            Number = number;
            Purpose = purpose;
        }
    }
}
