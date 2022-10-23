namespace HospitalLibrary.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FloorDetailsDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Purpose { get; set; }

        public FloorDetailsDTO(int id, int number, string purpose)
        {
            Id = id;
            Number = number;
            Purpose = purpose;
        }
    }
}
