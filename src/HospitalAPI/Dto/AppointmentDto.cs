namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.AppUsers;
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

        public ApplicationPatientDTO Patient { get; set; }

        public ApplicationDoctorDTO Doctor { get; set; }

        public bool Deleted { get; set; }
    }
}
