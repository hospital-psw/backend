namespace HospitalAPI.Dto.Examinations
{
    using System;

    public class AnamnesisStatisticsDto
    {
        public DateTime ExaminationDate { get; set; }

        public AppointmentDto Appointment { get; set; }

        public DateTime TimeSpan { get; set; }

        public int Version { get; set; }

        public AnamnesisStatisticsDto(DateTime examinationDate, AppointmentDto appointment, DateTime timeSpan, int version)
        {
            ExaminationDate = examinationDate;
            Appointment = appointment;
            TimeSpan = timeSpan;
            Version = version;
        }
    }
}
