namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationDataDto
    {
        public DateTime ExaminationDate { get; set; }

        public string Doctor { get; set; }

        public string Patient { get; set; }

        public string Specialization { get; set; }

        public string Type { get; set; }

        public int Duration { get; set; }

        public int Steps { get; set; }

        public ExaminationDataDto() { }

        public ExaminationDataDto(DateTime examinationDate, string doctor, string patient, string specialization, string type, int duration, int steps)
        {
            ExaminationDate = examinationDate;
            Doctor = doctor;
            Patient = patient;
            Specialization = specialization;
            Type = type;
            Duration = duration;
            Steps = steps;
        }
    }
}
