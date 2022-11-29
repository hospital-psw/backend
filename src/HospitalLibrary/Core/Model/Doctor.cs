namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Doctor : User
    {
        public Specialization Specialization { get; set; }

        public WorkingHours WorkHours { get; set; }

        public Room Office { get; set; }

        public DoctorSchedule DoctorSchedule { get; set; }

        public Doctor() { }
        public Doctor(string firstName, string lastName, string password, string email, Specialization specialization, WorkingHours workHours, Room office, DoctorSchedule doctorSchedule) : base(firstName, lastName, email, password, Role.DOCTOR)
        {
            Specialization = specialization;
            WorkHours = workHours;
            Office = office;
            DoctorSchedule = doctorSchedule;
        }
    }
}
