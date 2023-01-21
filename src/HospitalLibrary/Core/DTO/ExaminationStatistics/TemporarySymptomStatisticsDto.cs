namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TemporarySymptomStatisticsDto
    {
        public string Name { get; set; }
        public int Occurrences { get; set; }
        public int AnamnesisNum { get; set; }

        public TemporarySymptomStatisticsDto() { }

        public TemporarySymptomStatisticsDto(string name, int occurrences, int anamnesisNum)
        {
            Name = name;
            Occurrences = occurrences;
            AnamnesisNum = anamnesisNum;
        }
    }
}
