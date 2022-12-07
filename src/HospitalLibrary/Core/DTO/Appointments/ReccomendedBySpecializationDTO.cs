namespace HospitalLibrary.Core.DTO.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReccomendedBySpecializationDTO
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Room { get; set; }

        public int Floor { get; set; }

        public string Building { get; set; }

        public ReccomendedBySpecializationDTO(int doctorId, string firstName, string lastName, DateTime date, int duration, string room, int floor, string building)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Date = date;
            Duration = duration;
            Room = room;
            Floor = floor;
            Building = building;
        }

        public ReccomendedBySpecializationDTO()
        {
        }
    }
}
