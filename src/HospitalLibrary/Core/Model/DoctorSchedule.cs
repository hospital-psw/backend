namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.VacationRequests;
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

        public bool IsDoctorAvailable(DateTime date)
        {
            if (IsDoctorOnVacation(date) || DoctorHasAppointment(date) || DoctorHasConsilium(date))
            {
                return false;
            } 
            return true;
        }

        private bool IsDoctorOnVacation(DateTime date)
        {
            foreach (VacationRequest vr in VacationRequests)
            {
                if (DateTime.Compare(vr.From, date) <= 0 && DateTime.Compare(vr.To, date) >= 0)
                    return true;
            }
            return false;
        }

        private bool DoctorHasAppointment(DateTime dateTime)
        {
            foreach (Appointment ap in Appointments)
            {
                if (DateTime.Compare(ap.Date, dateTime) == 0)
                    return true;
            }
            return false;
        }

        private bool DoctorHasConsilium(DateTime dateTime)
        {
            foreach (Consilium consilium in Consiliums)
            {
                if (DateTime.Compare(consilium.DateTime, dateTime) == 0)
                    return true;
            }
            return false;
        }

    }
}
