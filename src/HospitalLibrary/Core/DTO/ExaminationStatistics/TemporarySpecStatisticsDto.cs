namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TemporarySpecStatisticsDto
    {
        public Specialization Specialization { get; set; }
        public int DurationSum { get; set; }
        public int AnamnesisNum { get; set; }

        public TemporarySpecStatisticsDto() { }

        public TemporarySpecStatisticsDto(Specialization specialization, int durationSum, int anamnesisNum)
        {
            Specialization = specialization;
            DurationSum = durationSum;
            AnamnesisNum = anamnesisNum;
        }
    }
}
