namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomStatisticsDto
    {
        public string Name { get; set; }

        public double Average { get; set; }

        public SymptomStatisticsDto() { }

        public SymptomStatisticsDto(string name, double average)
        {
            Name = name;
            Average = average;
        }
    }
}
