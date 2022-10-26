namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
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

        public FloorDetailsDTO(Floor floor)
        {
            Id = floor.Id;
            Number = floor.Number;
            Purpose = floor.Purpose;
        }
    }
}
