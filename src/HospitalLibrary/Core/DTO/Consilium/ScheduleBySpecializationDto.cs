namespace HospitalLibrary.Core.DTO.Consilium
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleBySpecializationDto
    {
        public int Presence { get; set; }
        public List<ApplicationDoctor> CurrentAppointmentAvailableDoctors { get; set; }

        public ScheduleBySpecializationDto(int presence, List<ApplicationDoctor> doctors)
        {
            Presence = presence;
            CurrentAppointmentAvailableDoctors = doctors;
        }
    }
}
