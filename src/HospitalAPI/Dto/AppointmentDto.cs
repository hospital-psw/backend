namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class AppointmentDto
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime EndDate { get; set; }

        public int Duration { get; set; }

        public ExaminationType ExamType { get; set; }

        public bool IsDone { get; set; }

        public RoomDto Room { get; set; }

        public PatientDto Patient { get; set; }

        public DoctorDto Doctor { get; set; }
    }
}
