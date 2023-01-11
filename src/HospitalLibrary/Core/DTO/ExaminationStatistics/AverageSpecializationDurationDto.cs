namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AverageSpecializationDurationDto
    {
        public List<ExaminationDurationDto> Examinations { get; set; }

        public List<SpecializationStatisticsDto> SpecializationsStatisticsList { get; set; }

        public AverageSpecializationDurationDto()
        {
            Examinations = new List<ExaminationDurationDto>();
        }

        public AverageSpecializationDurationDto(List<ExaminationDurationDto> examinations, List<SpecializationStatisticsDto> specializationsStatisticsList)
        {
            Examinations = examinations;
            SpecializationsStatisticsList = specializationsStatisticsList;
        }
    }
}
