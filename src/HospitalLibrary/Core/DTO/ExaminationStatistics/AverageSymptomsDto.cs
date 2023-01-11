namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AverageSymptomsDto
    {
        public List<SymptomStatisticsDto> SymptomStatisticsList { get; set; }

        public AverageSymptomsDto()
        {
            SymptomStatisticsList = new List<SymptomStatisticsDto>();
        }

        public AverageSymptomsDto(List<SymptomStatisticsDto> symptomStatisticsList)
        {
            SymptomStatisticsList = symptomStatisticsList;
        }
    }
}
