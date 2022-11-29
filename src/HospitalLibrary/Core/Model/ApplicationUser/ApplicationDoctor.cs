namespace HospitalLibrary.Core.Model.ApplicationUser
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctor : ApplicationUser
    {
        public ApplicationDoctor() : base() { }
        public ApplicationDoctor(string firstName, string lastName, DateTime dateOfBirth, Gender gender, Specialization specialization, WorkingHours workingHours, Room office, DoctorSchedule doctorSchedule) : base(firstName, lastName, dateOfBirth, gender)
        {
            Specialization = specialization;
            WorkHours = workingHours;
            Office = office;
            DoctorSchedule = doctorSchedule;
        }

        public Specialization Specialization { get; set; }
        public WorkingHours WorkHours { get; set; }
        public Room Office { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; }
    }
}
