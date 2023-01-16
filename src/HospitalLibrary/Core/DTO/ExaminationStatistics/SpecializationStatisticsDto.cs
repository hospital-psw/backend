namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpecializationStatisticsDto
    {
        public double AverageDuration { get; set; }
        public Specialization Specialization { get; set; }

        public SpecializationStatisticsDto() { }

        public SpecializationStatisticsDto(double averageDuration, Specialization specialization)
        {
            AverageDuration = averageDuration;
            Specialization = specialization;
        }
    }
}
