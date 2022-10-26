namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class RescheduleAppointmentDto
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public ExaminationType ExamType { get; set; }

        public int RoomId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
    }
}