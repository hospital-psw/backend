namespace HospitalLibrary.Core.DTO.Appointments
{
    using Microsoft.AspNetCore.Routing.Constraints;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecommendedAppointmentDto
    {
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Room { get; set; }

        public int Floor { get; set; }

        public string Building { get; set; }

        public RecommendedAppointmentDto() { }

        public RecommendedAppointmentDto(DateTime date, string room, int floor, string building)
        {
            Date = date;
            Room = room;
            Floor = floor;
            Building = building;
        }
    }
}
