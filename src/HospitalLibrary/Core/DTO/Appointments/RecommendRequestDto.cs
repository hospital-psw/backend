namespace HospitalLibrary.Core.DTO.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecommendRequestDto
    {
        public DateTime Date { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public RecommendRequestDto(DateTime date, int patientId, int doctorId)
        {
            Date = date;
            PatientId = patientId;
            DoctorId = doctorId;
        }
    }

}
