namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class AppointmentDisplayDto
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public ExaminationType ExaminationType { get; set; }

        public AppointmentDisplayDto(DateTime date, int duration, ExaminationType examinationType)
        {
            Date = date;
            Duration = duration;
            ExaminationType = examinationType;
        }

        public AppointmentDisplayDto()
        {
        }
    }
}
