using HospitalLibrary.Core.DTO;
using System;

namespace HospitalLibrary.Core.Model
{
    public class Room : Entity
    {
        public string Number { get; set; }
        public Floor Floor { get; set; }
        public Building Building { get; set; }
        public string Purpose { get; set; }
        public WorkingHours WorkingHours { get; set; }

        public Room()
        {
        }

        public Room(RoomDTO dto)
        {
            Number = dto.Number;
            Purpose = dto.Purpose;
            WorkingHours = new WorkingHours(dto.WorkigHoursDTO);
        }

    }
}