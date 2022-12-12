namespace HospitalAPI.Dto
{
    using System;

    public class AppointmentDoctorDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public AppointmentDoctorDto(int doctorId, int patientId, DateTime fromDate, DateTime toDate)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }

       // "doctorId": 0,
       // "patientId": 0,
       // "fromDate": "2022-12-11T23:23:05.017Z",
       // "toDate": "2022-12-12T23:23:05.017Z"

}
