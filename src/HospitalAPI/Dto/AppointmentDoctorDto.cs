namespace HospitalAPI.Dto
{
    using System;
    using System.Collections.Generic;

    public class AppointmentDoctorDto
    {
        public List<int> DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public AppointmentDoctorDto(List<int> doctorId, int patientId, DateTime fromDate, DateTime toDate)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public AppointmentDoctorDto()
        {
        }
    }

       // "doctorId": 0,
       // "patientId": 0,
       // "fromDate": "2022-12-11T23:23:05.017Z",
       // "toDate": "2022-12-12T23:23:05.017Z"

}
