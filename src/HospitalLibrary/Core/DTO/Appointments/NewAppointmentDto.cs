namespace HospitalLibrary.Core.DTO.Appointments
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewAppointmentDto
    {
        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public ExaminationType ExamType { get; set; }

    }
}
