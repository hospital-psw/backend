namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class AppointmentDto
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public ExaminationType ExamType { get; set; }

        public bool IsDone { get; set; }

        public RoomDto Room { get; set; }
    }
}