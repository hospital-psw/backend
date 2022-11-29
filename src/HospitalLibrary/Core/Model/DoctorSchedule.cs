namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorSchedule : Entity
    {
        public ApplicationDoctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<VacationRequest> VacationRequests { get; set; }
        public List<Consilium> Consiliums { get; set; }

        public DoctorSchedule() 
        { 
            Appointments = new List<Appointment>();
            VacationRequests = new List<VacationRequest>();
            Consiliums = new List<Consilium>();
        }

        public DoctorSchedule(ApplicationDoctor doctor, List<Appointment> appointments, List<VacationRequest> vacationRequests, List<Consilium> consiliums)
        {
            Doctor = doctor;
            Appointments = appointments;
            VacationRequests = vacationRequests;
            Consiliums = consiliums;
        }
    }
}
