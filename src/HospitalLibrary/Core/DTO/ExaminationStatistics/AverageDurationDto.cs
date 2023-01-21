namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AverageDurationDto
    {
        public List<ExaminationDurationDto> Examinations { get; set; }

        public double AverageDuration { get; set; }

        public AverageDurationDto()
        {
            Examinations = new List<ExaminationDurationDto>();
        }

        public AverageDurationDto(List<ExaminationDurationDto> examinations, double averageDuration)
        {
            Examinations = examinations;
            AverageDuration = averageDuration;
        }
    }
}
